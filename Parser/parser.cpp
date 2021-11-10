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


long long int readnum(vector<int> be, int bajthossz) {
	long long int count = 0;
	for (int i = bajthossz - 1; i >= 0; i--) {
		int egesz = be[i] / 16;
		int egyes = be[i] - egesz * 16;
		count += pow(16, 2 * i + 1) * egesz;
		count += pow(16, 2 * i) * egyes;
	}
	return count;
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

		result += "]}";
	}
	result += "]}";
	return result;
}


CiffData CiffParse(std::vector<uint8_t> contents, long long start) {
	long long  bytecount = start + 4;     //CIFF átugrása
	//read header size
	std::vector<int> buff = {};
	for (long long i = bytecount; i < bytecount + 8; ++i) {
		int i2 = contents[i];
		buff.push_back(i2);
	}
	bytecount = bytecount + 8;
	long long header_size = readnum(buff, 8);
	buff = {};
	for (long long  i = bytecount; i < bytecount + 8; ++i) {
		int i2 = contents[i];
		buff.push_back(i2);
	}
	bytecount = bytecount + 8;
	long long content_size = readnum(buff, 8);
	buff = {};

	for (long long  i = bytecount; i < bytecount + 8; ++i) {
		int i2 = contents[i];
		buff.push_back(i2);
	}
	bytecount = bytecount + 8;
	long long width = readnum(buff, 8);
	buff = {};

	for (long long i = bytecount; i < bytecount + 8; ++i) {
		int i2 = contents[i];
		buff.push_back(i2);
	}
	bytecount = bytecount + 8;
	long long height = readnum(buff, 8);
	buff = {};
	cout << "header s: " << header_size << " cont " << content_size << " width: " << width << " height: " << height;
	long long eleje = bytecount;
	for (bytecount; !(contents[bytecount] == 0x0A); bytecount++) {

	}
	//TODO kivágni és to string: contents[eleje]-tól contents[bytecount]-ig.

	std::vector<uint8_t> kep = {};
	int alpha = 2;
	for (long long int i = start+header_size; i < start+header_size + content_size; i++) {
		if (alpha == 2) {
			alpha = 0;
		}
		else {
			alpha++;
		}
		kep.push_back(contents[i]);
		if (alpha == 2) {
			kep.push_back(int8_t(255));
		}
	}
	struct CiffData kimenet;
	kimenet.height = height;
	kimenet.width = width;
	kimenet.caption = "teszt"; //TODO
	kimenet.pixeldata = kep;
	std::vector<std::string> tags = {}; //TODO
	kimenet.tags = tags;
	return kimenet;
}



long long readBytesAsInt(long startIndex, int length)
{
	long long result = 0;
	long long pos = 0;
	for (long i = startIndex; i < startIndex + length; i++) {
		result += ((long long)caffFileData[i]) << (pos * 8);
		pos++;
	}
	return result;
}

string readBytesAsString(long startIndex, int length)
{
	string result = "";
	for (long i = startIndex; i < startIndex + length; i++) {
		char c = (char)caffFileData[i];
		result += c;
	}
	return result;
}



void read_caff_header_data(long startIndex)
{
	if (!(caffFileData[startIndex] == 'C' && caffFileData[startIndex + 1] == 'A' && caffFileData[startIndex + 2] == 'F' && caffFileData[startIndex + 3] == 'F')) {
		cerr << "CAFF magic string missing!";
	}
	long long caff_header_size = readBytesAsInt(startIndex + 4, 8); // ez amúgy tök fölös?
	num_anim = readBytesAsInt(startIndex + 12, 8);
	cout << "num_anim:" << (int)num_anim << std::endl;
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

	// TODO itt egy CiffData structtal visszatérő függvény állítsa be a caff datát (caffFileData[startIndex] + 8 -nál kezdődik a CIFF data)
	startIndex += 8;
	ciffdata = CiffParse(caffFileData, startIndex);
	ciffdata.tags.push_back("asd");
	ciffdata.duration_milisecs = duration_milisecs;
	ciffDatas.push_back(ciffdata);
}

Blockheader read_block_header(int startIndex) {
	int blocktype = (int)caffFileData[startIndex];
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
			cerr << "There's more than 1 header block";
		}
		header_parsed = true;
		read_caff_header_data(startIndex + 9);
	}
	else if (blockheader.blocktype == 2) {
		if (!header_parsed)
		{
			cerr << "The first block must be header block!";
		}
		if (credits_parsed)
		{
			cerr << "There's more than 1 credits block!";
		}

		credits_parsed = true;
		read_caff_credits_data(startIndex + 9);
	}
	else if (blockheader.blocktype == 3) {
		if (!header_parsed)
		{
			cerr << "The first block must be header block!";
		}
		read_caff_animation_data(startIndex + 9);
		parsed_frames++;
	}
	else {
		cerr << "The block type must be 1, 2 or 3!";
	}
	return startIndex + 9 + blockheader.blocklength;
}




int main()
{
	std::ifstream stream("3.caff", std::ios::in | std::ios::binary);
	caffFileData = std::vector<uint8_t>(std::istreambuf_iterator<char>(stream), std::istreambuf_iterator<char>());
	long long fileByteCount = caffFileData.size();
	long long position = 0;
	do {
		position = read_block(position);
	} while (position < fileByteCount);

	if (parsed_frames != num_anim) {
		cerr << "Num_anim does not match CIFF blocks count";
	}
	string json = getJsonData();
	std::ofstream outfile("out.json");
	outfile << json;

	// TODO ha 0 frame?
	GifWriter g;
	if (ciffDatas.size() > 0) {
		CiffData cd = ciffDatas[0];
		//GifBegin(&g, "outgif.gif", 1000, 667, 100);
		GifBegin(&g, "outgif.gif", cd.width, cd.height, cd.duration_milisecs/10);
	}
	for (int i = 0; i < ciffDatas.size(); i++) {
		CiffData cd = ciffDatas[i];
		GifWriteFrame(&g, cd.pixeldata.data(), cd.width, cd.height, cd.duration_milisecs/10);
		//GifWriteFrame(&g, cd.pixeldata.data(), 1000, 667, 100);
		cout << endl << "Duration:" << cd.duration_milisecs << "capt: " << cd.caption;
	}
	GifEnd(&g);

	return 0;
}

