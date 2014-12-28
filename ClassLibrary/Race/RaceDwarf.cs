using ClassLibrary.Map;
using ClassLibrary.Player;
using ClassLibrary.Tile;
using ClassLibrary.Unit;

namespace ClassLibrary.Race
{
    public class RaceDwarf : IDRace
    {
        public string GetName()
        {
            return "nain";
        }

        public IDUnit CreateUnit(IDPlayer player, IPosition position, ITile tile)
        {
            return new UnitDwarf(player, position, tile);
        }
    }
}
