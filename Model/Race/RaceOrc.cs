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
        public int ID
        {
            get { return 2; }
        }

        public string Name
        {
            get { return "orc"; }
        }

        public IDUnit CreateUnit(IDPlayer player, IPosition position, ITile tile)
        {
            return new UnitOrc(player, position, tile);
        }
    }
}
