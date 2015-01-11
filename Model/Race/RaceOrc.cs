using System;
using Model.Map;
using Model.Player;
using Model.Tile;
using Model.Unit;

namespace Model.Race
{
    [Serializable()]
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
