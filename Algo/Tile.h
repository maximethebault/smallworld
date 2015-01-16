#pragma once

#ifndef TILE_H
#define TILE_H

#include <cmath>

class Tile
{
	int _x;
	int _y;
	float _cost;
public:
	Tile(int x, int y, float cost);
	~Tile();
	static int distance(int x, int y, int oX, int oY);
	static bool isAdjacent(int x, int y, int oX, int oY);
	int getX();
	int getY();
	float getCost();
	float score;
	bool suggested;
};

#endif