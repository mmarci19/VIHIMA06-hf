#include <vector>
#include <cstdint>
#include <string>
#include "gif.h"

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

	return 0;
}

int getPixelPosFromXY(int width, int height, int x, int y)
{


}
