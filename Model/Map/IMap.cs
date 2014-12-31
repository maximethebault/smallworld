using Model.Tile;

namespace Model.Map
{
    public interface IMap
    {
        ITile TileAtPosition(IPosition position);
    }
}