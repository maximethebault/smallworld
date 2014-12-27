#pragma once

#define DLL _declspec(dllexport)
#define EXTERNC extern "C"

#include <iostream>
#include <stdio.h>
#include <random>

class DLL Algo {
	private:
		int ** _map;
		int ** _playerPlacement;
		int _mapSize;
		int _nbPlayers;
		int _nbTileTypes;
	public:
		Algo(int mapSize, int nbPlayers, int nbTileTypes);
		~Algo();
		int ** createMap();
		int ** placePlayers();
};

// définition d'une sorte d'interface

EXTERNC DLL Algo* Algo_new(int mapSize, int nbPlayers, int nbTileTypes);
EXTERNC DLL void Algo_delete(Algo* algo);

EXTERNC DLL int ** Algo_createMap(Algo* algo);
EXTERNC DLL int ** Algo_placePlayers(Algo* algo);