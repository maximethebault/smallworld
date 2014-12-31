using Model.Map;
using Model.Player;
using Model.Tile;
using Model.Unit;

namespace Model.Race
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
