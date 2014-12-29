#ifndef __WRAPPER__
#define __WRAPPER__

#include "../Algo/Algo.h"

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
	};
}

#endif