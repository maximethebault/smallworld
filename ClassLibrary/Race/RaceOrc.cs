using ClassLibrary.Map;
using ClassLibrary.Player;
using ClassLibrary.Tile;
using ClassLibrary.Unit;

namespace ClassLibrary.Race
{
    public class RaceOrc : IDRace
    {
        public string GetName()
        {
            return "orc";
        }

        public IDUnit CreateUnit(IDPlayer player, IPosition position, ITile tile)
        {
            return new UnitOrc(player, position, tile);
        }
    }
}
