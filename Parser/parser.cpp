#include <vector>
#include <cstdint>
#include <string>
#include "gif.h"
#include <iostream>
#include <fstream>
using namespace std;



std::vector<uint8_t> caffFileData;
long long num_anim = 0;
bool header_parsed = false;
bool credits_parsed = false;

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

struct Blockheader {
	int blocktype;
	long long blocklength;
};


void read_caff_header_data(long startIndex)
{
	if (!(caffFileData[startIndex] == 'C' && caffFileData[startIndex + 1] == 'A' && caffFileData[startIndex + 2] == 'F' && caffFileData[startIndex + 3] == 'F')) {
		//TODO hiba: nincs magic CAFF
		cout << "BAJ!";
	}
	long long caff_header_size = readBytesAsInt(startIndex + 4, 8); // ez amúgy tök fölös?
	cout << "headerBlockSize:" << (int)caff_header_size << std::endl;
	num_anim = readBytesAsInt(startIndex + 12, 8);
	cout << "num_anim:" << (int)num_anim << std::endl;
}

void read_caff_credits_data(long startIndex)
{
	long long year = readBytesAsInt(startIndex, 3);

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
		if (header_parsed) {
			cout << "BAJ1!";
		}
		header_parsed = true;
		read_caff_header_data(startIndex + 9);
	}
	else if (blockheader.blocktype == 2) {
		if (header_parsed || credits_parsed) {
			cout << "BAJ2!";
		}
		credits_parsed = true;
		read_caff_credits_data(startIndex + 9);
	}
	else if (blockheader.blocktype == 3) {

	}
	else {
		// TODO hiba: nem 1,2,3 a block jelzo
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

	// for(auto i: caffFileData) {
	// 	int value = i;
	// 	std::cout << "data: " << value << std::endl;
	// 	return 0;
	// }

	int res = read_block(0);
	read_block(res);


	uint8_t elsoByte = caffFileData[0];

	return 0;
}

