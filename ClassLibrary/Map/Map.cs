using ClassLibrary.Tile;

namespace ClassLibrary.Map
{
    public class Map : IDMap
    {
        public Tile.Tile[,] TileMap
        {
            get;
            set;
        }
        private int _mapWidth;



        public Map(int mapWidth)
        {
            TileMap = new Tile.Tile[mapWidth, mapWidth];
            _mapWidth = mapWidth;
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