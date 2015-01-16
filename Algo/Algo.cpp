#include "Algo.h"

using namespace std;

Algo::Algo(int mapSize, int nbPlayers, int nbTileTypes) : _mapSize(mapSize), _nbPlayers(nbPlayers), _nbTileTypes(nbTileTypes) {
}

Algo::~Algo() {
	this->destroyMap();
	this->destroyPlayerPlacement();
}

void Algo::destroyMap() {
	if (_map) {
		for (int i = 0; i < _mapSize; i++) {
			delete[] _map[i];
		}
		delete[] _map;
	}
}

void Algo::destroyPlayerPlacement() {

	if (_playerPlacement) {
		for (int i = 0; i < _nbPlayers; i++) {
			delete[] _playerPlacement[i];
		}
		delete[] _playerPlacement;
	}
}

void Algo::destroyAdvices() {
	if (_advices) {
		for (int i = 0; i < 3; i++) {
			delete[] _advices[i];
		}
		delete[] _advices;
	}
}

int ** Algo::createMap() {
	//first, make sure the old pointer is deleted
	destroyMap();

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
	//first, make sure the old pointer is deleted
	destroyPlayerPlacement();

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

vector<Tile*> Algo::reachableTiles(int * position, float moveLeft, int type, int ** map, int ** units) {
	vector<Tile*> reachableTiles;

	for (int i = 0; i < _mapSize; i++) {
		for (int j = 0; j < _mapSize; j++) {
			if (i == position[0] && j == position[1]) {
				continue;
			}
			if (!Tile::isAdjacent(position[0], position[1], i, j) && !(type == 1 && map[i][j] == 2 && (units[i][j] == -1 || units[i][j] == 1))) {
				continue;
			}
			float cost;

			// movement cost calculation
			// elf
			if (type == 0 && map[i][j] == 1) {
				// forest
				cost = 0.5;
			}
			else if (type == 0 && map[i][j] == 0) {
				// desert
				cost = 2;
			}
			// dwarf
			else if (type == 1 && map[i][j] == 3) {
				// plain
				cost = 0.5;
			}
			// orc
			else if (type == 2 && map[i][j] == 3) {
				// plain
				cost = 0.5;
			}
			else {
				cost = 1;
			}

			if (cost > moveLeft) {
				continue;
			}
			reachableTiles.push_back(new Tile(i, j, cost));
		}
	}

	return reachableTiles;
}

void Algo::scoreMove(vector<Tile*> tiles, int * position, float moveLeft, int type, int ** map, int ** units) {
	vector<Tile*>::iterator it;
	// score: the higher, the better!

	// stay close to your ennemies!
	for (int i = 0; i < _mapSize; i++) {
		for (int j = 0; j < _mapSize; j++) {
			if (units[i][j] != type) {
				// find which tile is the closest to the ennemy
				int minDistance = 1000;
				Tile* winner = 0;
				for (it = tiles.begin(); it != tiles.end(); it++) {
					int distance = Tile::distance((*it)->getX(), (*it)->getY(), i, j);
					if (distance < minDistance) {
						minDistance = distance;
						winner = *it;
					}
				}
				winner->score -= minDistance;
			}
		}
	}

	for (it = tiles.begin(); it != tiles.end(); it++) {
		(*it)->score += 1-(*it)->getCost();
	}
	sort(tiles.begin(), tiles.end(), compByScore);
	float oldScore = tiles.at(0)->score;
	int i;
	for (it = tiles.begin(), i = 0; it != tiles.end() && i < 3; it++, i++) {
		(*it)->suggested = true;
		if (oldScore - (*it)->score > 3) {
			break;
		}
	}
}



// position: the position of the unit that we'd like adivces for
// type: the type of the unit that we'd like adivces for
// map: for each position, identifies the tile type
// units: for each position, identifies the unit type
int ** Algo::advice(int * position, float moveLeft, int type, int ** map, int ** units) {
	destroyAdvices();

	_advices = new int*[3];
	for (int i = 0; i < 3; i++) {
		// no more than 3 suggested position
		_advices[i] = new int[3];
	}

	vector<Tile*> tiles = this->reachableTiles(position, moveLeft, type, map, units);
	if (tiles.size())
	{
		this->scoreMove(tiles, position, moveLeft, type, map, units);
		vector<Tile*>::iterator it;
		int i;
		for (it = tiles.begin(), i = 0; it != tiles.end(); it++) {
			if ((*it)->suggested) {
				_advices[i][0] = (*it)->getX();
				_advices[i][1] = (*it)->getY();
				i++;
			}
		}
	}
	return _advices;
}

int Algo::getMapSize() {
	return _mapSize;
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

int ** Algo_advice(Algo* algo, int * position, float moveLeft, int type, int ** map, int ** units) {
	return algo->advice(position, moveLeft, type, map, units);
}

int Algo_mapSize(Algo* algo) {
	return algo->getMapSize();
}

bool compByScore(Tile* a, Tile* b)
{
	return a->score < b->score;
}