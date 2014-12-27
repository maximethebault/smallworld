using ClassLibrary.Tile;

namespace ClassLibrary.Map
{
    public class Map : IMap
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
            TileMap[position.GetX(), position.GetY()] = tile;
        }

        public ITile GetTileAtPosition(IPosition position)
        {
            return TileMap[position.GetX(), position.GetY()];
        }

    }
}