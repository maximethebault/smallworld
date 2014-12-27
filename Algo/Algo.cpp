#include "Algo.h"

using namespace std;

Algo::Algo(int mapSize, int nbPlayers, int nbTileTypes) : _mapSize(mapSize), _nbPlayers(nbPlayers), _nbTileTypes(nbTileTypes) {
}

Algo::~Algo() {
	for (int i = 0; i < _mapSize; i++) {
		delete[] _map[i];
	}
	delete[] _map;

	for (int i = 0; i < _nbPlayers; i++) {
		delete[] _playerPlacement[i];
	}
	delete[] _playerPlacement;
}

int ** Algo::createMap() {
	std::mt19937 eng{ std::random_device{}() };
	std::uniform_int_distribution<> dist{ 0, _nbTileTypes-1 };

	// map init
	_map = new int *[_mapSize];
	for (int i = 0; i < _mapSize; i++) {
		_map[i] = new int[_mapSize];
	}
	int nbTiles = _mapSize * _mapSize;


	int * nbTileTypeLeft = new int[_nbTileTypes];
	for (int j = 0; j < _nbTileTypes; j++) {
		nbTileTypeLeft[j] = ceil((double) nbTiles / _nbTileTypes);
	}

	// map fill
	for (int i = 0; i < _mapSize; i++) {
		for (int j = 0; j < _mapSize; j++) {
			int randomTileType = dist(eng);
			while (nbTileTypeLeft[randomTileType] == 0) {
				randomTileType = dist(eng);
			}
			_map[i][j] = randomTileType;
			nbTileTypeLeft[randomTileType]--;
		}
	}
	return _map;
}

int ** Algo::placePlayers() {
	// player placement init
	_playerPlacement = new int *[_nbPlayers];
	for (int i = 0; i < _nbPlayers; i++) {
		// size 2 array: for the coordinates
		_playerPlacement[i] = new int[2];
	}

	_playerPlacement[0][0] = 0;
	_playerPlacement[0][1] = 0;

	_playerPlacement[1][0] = _mapSize-1;
	_playerPlacement[1][1] = _mapSize-1;

	if (_nbPlayers > 2) {
		_playerPlacement[2][0] = 0;
		_playerPlacement[2][1] = _mapSize - 1;
	}

	if (_nbPlayers > 3) {
		_playerPlacement[3][0] = _mapSize - 1;
		_playerPlacement[3][1] = 0;
	}

	return _playerPlacement;
}

// interface

Algo* Algo_new(int mapSize, int nbPlayers, int nbTileTypes) {
	return new Algo(mapSize, nbPlayers, nbTileTypes);
}

void Algo_delete(Algo* algo) {
	delete algo;
}

int ** Algo_createMap(Algo* algo) {
	return algo->createMap();
}

int ** Algo_placePlayers(Algo* algo) {
	return algo->placePlayers();
}