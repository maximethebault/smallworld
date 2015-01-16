#include "Tile.h"

using namespace std;

Tile::Tile(int x, int y, float cost) : _x(x), _y(y), _cost(cost)
{
	score = 0;
	suggested = false;
}


Tile::~Tile()
{
}

int Tile::distance(int x, int y, int oX, int oY) {
	return (abs(x - oX) + abs(y - oY));
}


bool Tile::isAdjacent(int x, int y, int oX, int oY)
{
	if (y == oY && abs(x - oX) == 1)
	{
		return true;
	}

	if (y % 2 == 0 && abs(y - oY) == 1 && (x == oX || x == oX + 1))
	{
		return true;
	}

	if (y % 2 == 1 && abs(y - oY) == 1 && (x == oX || x == oX - 1))
	{
		return true;
	}
	return false;
}


int Tile::getX()
{
	return _x;
}


int Tile::getY()
{
	return _y;
}

float Tile::getCost()
{
	return _cost;
}