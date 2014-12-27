using ClassLibrary.Tile;

namespace ClassLibrary.Map
{
    public interface IMap
    {
        ITile GetTileAtPosition(IPosition position);
    }
}