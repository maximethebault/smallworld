#include "CliConverter.h"

using namespace Wrapper;

CliConverter::CliConverter()
{
}

int ** CliConverter::arrayToPointer(array<int, 2>^ tab, int size) {
	int** line = new int*[size];
	for (int i = 0; i < size; i++) {
		pin_ptr<int> p = &tab[i,0];
		line[i] = p;
	}
	return line;
}