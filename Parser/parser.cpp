#include <vector>
#include <cstdint>
#include <string>
#include "gif.h"
#include <iostream>
#include <fstream>
using namespace std;




struct Blockheader {
	int blocktype;
	long long blocklength;
};

struct CiffData {
	long long width;
	long long height;
	long long duration_milisecs;
	string caption;
	std::vector<string> tags;
	std::vector<uint8_t> pixeldata;
};

std::vector<uint8_t> caffFileData;
long long num_anim = 0;
long long year = 0;
long long month = 0;
long long day = 0;
long long hour = 0;
long long minute = 0;
string creator = "";
bool header_parsed = false;
bool credits_parsed = false;
int parsed_frames = 0;

std::vector<CiffData> ciffDatas;

long long readBytesAsInt(long startIndex, int length)
{
	long long result = 0;
	long long pos = 0;
	for (long i = startIndex; i < startIndex + length; i++) {
		result += ((long long)caffFileData.at(i)) << (pos * 8);
		pos++;
	}
	return result;
}

string readBytesAsString(long startIndex, int length)
{
	string result = "";
	for (long i = startIndex; i < startIndex + length; i++) {
		char c = (char)caffFileData.at(i);
		result += c;
	}
	return result;
}


// Source: https://stackoverflow.com/questions/180947/base64-decode-snippet-in-c
static std::string base64_encode(const std::string& in) {

	std::string out;

	int val = 0, valb = -6;
	for (char c : in) {
		val = (val << 8) + c;
		valb += 8;
		while (valb >= 0) {
			out.push_back("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"[(val >> valb) & 0x3F]);
			valb -= 6;
		}
	}
	if (valb > -6) out.push_back("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"[((val << 8) >> (valb + 8)) & 0x3F]);
	while (out.size() % 4) out.push_back('=');
	return out;
}

void parsing_error(string message) {	
	cerr << message << std::endl;
	throw "error";
}


string getJsonData() {

	string result = "";
	result += "{ \"numAnim\":" + std::to_string(num_anim);
	result += ", \"year\":" + std::to_string(year);
	result += ", \"month\":" + std::to_string(month);
	result += ", \"day\":" + std::to_string(day);
	result += ", \"hour\":" + std::to_string(hour);
	result += ", \"minute\":" + std::to_string(minute);
	result += ", \"creatorB64\":\"" + base64_encode(creator) + "\"";

	result += ", \"ciffs\":[";
	for (int i = 0; i < ciffDatas.size(); i++) {
		CiffData ciffData = ciffDatas[i];
		result += "{ \"duration\":" + std::to_string(ciffData.duration_milisecs);
		result += ", \"width\":" + std::to_string(ciffData.width);
		result += ", \"height\":" + std::to_string(ciffData.height);
		result += ", \"captionB64\":\"" + base64_encode(ciffData.caption) + "\"";
		result += ", \"tagB64s\":[";
		for (int j = 0; j < ciffData.tags.size(); j++) {
			result += "\"" + base64_encode(ciffData.tags[j]) + "\",";
		}
		result += "]},";
	}
	result += "]}";
	return result;
}


CiffData CiffParse(long long startIndex) {
	if (!(caffFileData.at(startIndex) == 'C' && caffFileData.at(startIndex + 1) == 'I' && caffFileData.at(startIndex + 2) == 'F' && caffFileData.at(startIndex + 3) == 'F')) {		
		parsing_error("CIFF magic string not present!");
	}

	long long header_size = readBytesAsInt(startIndex + 4, 8);
	long long header_end = startIndex + header_size;
	long long content_size = readBytesAsInt(startIndex + 12, 8);
	long long width = readBytesAsInt(startIndex + 20, 8);
	long long height = readBytesAsInt(startIndex + 28, 8);
	long long currentPosition = startIndex + 36;
	string caption = "";
	for (currentPosition; !(caffFileData.at(currentPosition) == 0x0A); currentPosition++) {
		caption += caffFileData.at(currentPosition);
	}
	currentPosition++;
	std::vector<string> tags;
	string tag = "";
	for (long long i = currentPosition; i < header_end; i++) {

		if (caffFileData.at(i) == '\0')
		{
			tags.push_back(tag);
			tag = "";
		}
		else
		{
			tag += caffFileData.at(i);
		}
	}
	std::vector<uint8_t> kep = {};
	int alpha = 2;
	for (long long int i = startIndex + header_size; i < startIndex + header_size + content_size; i++) {
		if (alpha == 2) {
			alpha = 0;
		}
		else {
			alpha++;
		}
		kep.push_back(caffFileData.at(i));
		if (alpha == 2) {
			kep.push_back(int8_t(255));
		}
	}
	struct CiffData kimenet;
	kimenet.height = height;
	kimenet.width = width;
	kimenet.caption = caption;
	kimenet.pixeldata = kep;
	kimenet.tags = tags;
	return kimenet;
}


void read_caff_header_data(long startIndex)
{
	if (!(caffFileData.at(startIndex) == 'C' && caffFileData.at(startIndex + 1) == 'A' && caffFileData.at(startIndex + 2) == 'F' && caffFileData.at(startIndex + 3) == 'F')) {		
		parsing_error("CAFF magic string missing!");
	}
	long long caff_header_size = readBytesAsInt(startIndex + 4, 8);
	num_anim = readBytesAsInt(startIndex + 12, 8);
}

void read_caff_credits_data(long startIndex)
{
	year = readBytesAsInt(startIndex, 2);
	month = readBytesAsInt(startIndex + 2, 1);
	day = readBytesAsInt(startIndex + 3, 1);
	hour = readBytesAsInt(startIndex + 4, 1);
	minute = readBytesAsInt(startIndex + 5, 1);

	long long lengthOfCreatorField = readBytesAsInt(startIndex + 6, 8);
	creator = readBytesAsString(startIndex + 14, lengthOfCreatorField);
}

void read_caff_animation_data(long startIndex)
{
	long long duration_milisecs = readBytesAsInt(startIndex, 8);
	CiffData ciffdata;

	// TODO itt egy CiffData structtal visszatérő függvény állítsa be a caff datát (caffFileData.at(startIndex] + 8 -nál kezdődik a CIFF data)
	startIndex += 8;
	ciffdata = CiffParse(startIndex);
	ciffdata.tags.push_back("asd");
	ciffdata.duration_milisecs = duration_milisecs;
	ciffDatas.push_back(ciffdata);
}

Blockheader read_block_header(int startIndex) {
	int blocktype = (int)caffFileData.at(startIndex);
	long long blocklength = readBytesAsInt(startIndex + 1, 8);
	Blockheader result;
	result.blocktype = blocktype;
	result.blocklength = blocklength;
	return result;
}

int read_block(long startIndex)
{
	Blockheader blockheader = read_block_header(startIndex);

	if (blockheader.blocktype == 1) {
		if (header_parsed)
		{
			parsing_error("There's more than 1 header block");
		}
		header_parsed = true;
		read_caff_header_data(startIndex + 9);
	}
	else if (blockheader.blocktype == 2) {
		if (!header_parsed)
		{
			parsing_error("The first block must be header block!");
		}
		if (credits_parsed)
		{
			parsing_error("There's more than 1 credits block!");
		}

		credits_parsed = true;
		read_caff_credits_data(startIndex + 9);
	}
	else if (blockheader.blocktype == 3) {
		if (!header_parsed)
		{
			parsing_error("The first block must be header block!");
		}
		read_caff_animation_data(startIndex + 9);
		parsed_frames++;
	}
	else {
		parsing_error("The block type must be 1, 2 or 3!");
	}
	return startIndex + 9 + blockheader.blocklength;
}


void convert_caff(string filename) {
	std::ifstream stream(filename, std::ios::in | std::ios::binary);
	caffFileData = std::vector<uint8_t>(std::istreambuf_iterator<char>(stream), std::istreambuf_iterator<char>());
	long long fileByteCount = caffFileData.size();
	long long position = 0;
	do {
		position = read_block(position);
	} while (position < fileByteCount);

	if (parsed_frames != num_anim) {
		parsing_error("Num_anim does not match CIFF blocks count");
	}
	string json = getJsonData();
	std::ofstream outfile("out.json");
	outfile << json;
	if (ciffDatas.size() == 0)
	{
		parsing_error("CAFF has 0 frames!");
	}
	GifWriter g;
	if (ciffDatas.size() > 0) {
		CiffData cd = ciffDatas[0];
		GifBegin(&g, "outgif.gif", cd.width, cd.height, cd.duration_milisecs / 10);
	}
	for (int i = 0; i < ciffDatas.size(); i++) {
		CiffData cd = ciffDatas[i];
		GifWriteFrame(&g, cd.pixeldata.data(), cd.width, cd.height, cd.duration_milisecs / 10);
	}
	GifEnd(&g);
}

int main(int argc, char** argv)
{
	string filename = "";
	if (argc >= 2)
	{
		filename = argv[1];
	}
	else
	{
		cerr << "No file given!" << std::endl;
	}

	try
	{
		convert_caff(filename);
		cout << "CONVERSION SUCCESFUL";
	}
	catch (...)
	{
		cerr << "Parsing error!" << std::endl;
	}

	return 0;
}

