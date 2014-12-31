using Model.Map;
using Model.Player;
using Model.Tile;
using Model.Unit;

namespace Model.Race
{
    public interface IDRace : IRace
    {
        IDUnit CreateUnit(IDPlayer player, IPosition position, ITile tile);
    }
}
