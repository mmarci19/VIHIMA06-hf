#include <vector>
#include <cstdint>
#include <string>
#include "gif.h"
#include <iostream>
#include <fstream>

int main()
{
	int width = 100;
	int height = 200;
	std::vector<uint8_t> black(width * height * 4, 0);
	std::vector<uint8_t> white(width * height * 4, 255);

	white[40000] = 0;
	white[40001] = 0;
	white[40002] = 0;
	white[40003] = 0;
	white[40004] = 0;

	auto fileName = "bwgif.gif";
	int delay = 100;
	GifWriter g;
	GifBegin(&g, fileName, width, height, delay);
	GifWriteFrame(&g, black.data(), width, height, delay);
	GifWriteFrame(&g, white.data(), width, height, delay);
	GifEnd(&g);


	std::ifstream stream("1.caff", std::ios::in | std::ios::binary);
	std::vector<uint8_t> contents((std::istreambuf_iterator<char>(stream)), std::istreambuf_iterator<char>());


	return 0;
}
