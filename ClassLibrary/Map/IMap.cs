using ClassLibrary.Tile;

namespace ClassLibrary.Map
{
    public interface IMap
    {
        ITile TileAtPosition(IPosition position);
    }
}