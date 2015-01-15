#include "Wrapper.h"

using namespace Wrapper;

WrapperAlgo::WrapperAlgo(int mapSize, int nbPlayers, int nbTileTypes) {
	_algo = Algo_new(mapSize, nbPlayers, nbTileTypes);
}

WrapperAlgo::~WrapperAlgo() {
	// TODO: make sure the destructor is called
	delete _algo;
	//Algo_delete(_algo);
	//delete _algo;
}

int ** WrapperAlgo::createMap() {
	return Algo_createMap(_algo);
}

int ** WrapperAlgo::placePlayers() {
	return Algo_placePlayers(_algo);
}