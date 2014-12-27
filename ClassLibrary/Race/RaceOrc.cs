using ClassLibrary.Map;
using ClassLibrary.Unit;

namespace ClassLibrary.Race
{
    public class RaceOrc : Race
    {
        public override string GetName()
        {
            return "orc";
        }

        public override Unit.Unit CreateUnit(Player.Player player, IPosition position, Tile.Tile tile)
        {
            return new UnitOrc(player, position, tile);
        }
    }
}
