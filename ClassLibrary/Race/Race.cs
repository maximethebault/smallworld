using ClassLibrary.Map;

namespace ClassLibrary.Race
{
    abstract public class Race : IRace
    {
        public abstract string GetName();

        public abstract Unit.Unit CreateUnit(Player.Player player, IPosition position, Tile.Tile tile);
    }
}
