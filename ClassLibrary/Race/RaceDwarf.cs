using ClassLibrary.Map;
using ClassLibrary.Unit;

namespace ClassLibrary.Race
{
    public class RaceDwarf : Race
    {
        public override string GetName()
        {
            return "nain";
        }

        public override Unit.Unit CreateUnit(Player.Player player, IPosition position, Tile.Tile tile)
        {
            return new UnitDwarf(player, position, tile);
        }
    }
}
