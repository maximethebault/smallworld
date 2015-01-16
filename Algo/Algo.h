#pragma once

#ifndef ALGO_H
#define ALGO_H

#define DLL _declspec(dllexport)
#define EXTERNC extern "C"

#include <iostream>
#include <stdio.h>
#include <random>
#include <vector>
#include <algorithm>
#include "Tile.h"

class DLL Algo {
	private:
		int ** _map;
		int ** _playerPlacement;
		int ** _advices;
		int _mapSize;
		int _nbPlayers;
		int _nbTileTypes;
		void destroyMap();
		void destroyPlayerPlacement();
		void destroyAdvices();
		std::vector<Tile*> reachableTiles(int * position, float moveLeft, int type, int ** map, int ** units);
	public:
		Algo(int mapSize, int nbPlayers, int nbTileTypes);
		~Algo();
		int ** createMap();
		int ** placePlayers();
		int getMapSize();
		void scoreMove(std::vector<Tile*> tiles, int * position, float moveLeft, int type, int ** map, int ** units);
		int ** advice(int * position, float moveLeft, int type, int ** map, int ** units);
};

// we define our library interface

EXTERNC DLL Algo* Algo_new(int mapSize, int nbPlayers, int nbTileTypes);
EXTERNC DLL void Algo_delete(Algo* algo);

EXTERNC DLL int ** Algo_createMap(Algo* algo);
EXTERNC DLL int ** Algo_placePlayers(Algo* algo);
EXTERNC DLL int ** Algo_advice(Algo* algo, int * position, float moveLeft, int type, int ** map, int ** units);

EXTERNC DLL int Algo_mapSize(Algo* algo);

bool compByScore(Tile* a, Tile* b);

#endif