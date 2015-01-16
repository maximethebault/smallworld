#ifndef __WRAPPER__
#define __WRAPPER__

#include "../Algo/Algo.h"
#include "CliConverter.h"

#include <iostream>

using namespace System;

namespace Wrapper {
	public ref class WrapperAlgo {
		private:
			Algo* _algo;
		public:
			WrapperAlgo(int mapSize, int nbPlayers, int nbTileTypes);
			~WrapperAlgo();
			int ** createMap();
			int ** placePlayers();
			int ** advice(array<int>^ position, float moveLeft, int type, array<int, 2>^ map, array<int, 2>^ units);
	};
}

#endif