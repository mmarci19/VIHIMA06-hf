#include <vector>
#include <cstdint>
#include <string>
#include "gif.h"
#include <iostream>
#include <fstream>
using namespace std;



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
	result += "{numAnim:" + num_anim + "}";
	return result;
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

struct Blockheader {
	int blocktype;
	long long blocklength;
};


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
	cerr << "TODO";
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
	int width = 100;
	int height = 200;
	std::vector<uint8_t> black(width * height * 4, 0);
	std::vector<uint8_t> white(width * height * 4, 255);

	auto fileName = "bwgif.gif";
	int delay = 100;
	GifWriter g;
	GifBegin(&g, fileName, width, height, delay);
	GifWriteFrame(&g, black.data(), width, height, delay);
	GifWriteFrame(&g, white.data(), width, height, delay);
	GifEnd(&g);


	std::ifstream stream("1_mod.caff", std::ios::in | std::ios::binary);
	caffFileData = std::vector<uint8_t>(std::istreambuf_iterator<char>(stream), std::istreambuf_iterator<char>());
	long long fileByteCount = caffFileData.size();
	long long position = 0;
	do {
		position = read_block(position);
	} while (position < fileByteCount);


	uint8_t elsoByte = caffFileData[0];

	return 0;
}

