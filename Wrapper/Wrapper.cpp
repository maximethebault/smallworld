#include "Wrapper.h"
#include "tools.h"

using namespace Wrapper;

WrapperAlgo::WrapperAlgo(int mapSize, int nbPlayers, int nbTileTypes) {
	_algo = Algo_new(mapSize, nbPlayers, nbTileTypes);
}

WrapperAlgo::~WrapperAlgo() {
	Algo_delete(_algo);
}

int ** WrapperAlgo::createMap() {
	return Algo_createMap(_algo);
}

int ** WrapperAlgo::placePlayers() {
	return Algo_placePlayers(_algo);
}

int ** WrapperAlgo::advice(array<int>^ position, float moveLeft, int type, array<int, 2>^ map, array<int, 2>^ units) {
	pin_ptr<int> pPosition = &position[0];
	int ** mapPtr = CliConverter::arrayToPointer(map, Algo_mapSize(_algo));
	int ** unitsPtr = CliConverter::arrayToPointer(units, Algo_mapSize(_algo));
	return Algo_advice(_algo, pPosition, moveLeft, type, mapPtr, unitsPtr);
}