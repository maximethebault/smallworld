using ClassLibrary.Map;
using ClassLibrary.Player;
using ClassLibrary.Tile;
using ClassLibrary.Unit;

namespace ClassLibrary.Race
{
    public class RaceElf : IDRace
    {
        public string GetName()
        {
            return "elf";
        }

        public IDUnit CreateUnit(IDPlayer player, IPosition position, ITile tile)
        {
            return new UnitElf(player, position, tile);
        }
    }
}
