using ClassLibrary.Map;
using ClassLibrary.Player;
using ClassLibrary.Tile;
using ClassLibrary.Unit;

namespace ClassLibrary.Race
{
    public interface IDRace : IRace
    {
        IDUnit CreateUnit(IDPlayer player, IPosition position, ITile tile);
    }
}
