#ifndef __WRAPPER__
#define __WRAPPER__

#include "../Algo/Algo.h"

#include <iostream>

#pragma comment(lib, "../Algo/Release/Algo.lib")

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
	};
}

#endif