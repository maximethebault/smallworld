#pragma once

namespace Wrapper {
	ref class CliConverter
	{
	public:
		CliConverter();
		static int ** CliConverter::arrayToPointer(array<int, 2>^ tab, int size);
	};
}