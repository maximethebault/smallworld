using System;
using Model.Tile;

namespace Model.Map
{
    [Serializable()]
    public class Map : IDMap
    {
        public Tile.Tile[,] TileMap
        {
            get;
            set;
        }

        public Map(int mapWidth)
        {
            TileMap = new Tile.Tile[mapWidth, mapWidth];
        }

        public void AddTile(IPosition position, Tile.Tile tile)
        {
            TileMap[position.X, position.Y] = tile;
        }

        public ITile TileAtPosition(IPosition position)
        {
            return TileMap[position.X, position.Y];
        }

    }
}