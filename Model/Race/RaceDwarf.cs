using System;
using Model.Map;
using Model.Player;
using Model.Tile;
using Model.Unit;

namespace Model.Race
{
    [Serializable()]
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
