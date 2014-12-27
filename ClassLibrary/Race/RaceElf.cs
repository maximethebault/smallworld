using ClassLibrary.Map;
using ClassLibrary.Unit;

namespace ClassLibrary.Race
{
    public class RaceElf : Race
    {
        public override string GetName()
        {
            return "elf";
        }

        public override Unit.Unit CreateUnit(Player.Player player, IPosition position, Tile.Tile tile)
        {
            return new UnitElf(player, position, tile);
        }
    }
}
