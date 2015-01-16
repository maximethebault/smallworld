using System;
using Model.Map;
using Model.Player;
using Model.Tile;
using Model.Unit;

namespace Model.Race
{
    [Serializable()]
    public class RaceElf : IDRace
    {
        public int ID
        {
            get { return 0; }
        }

        public string Name
        {
            get { return "elf"; }
        }

        public IDUnit CreateUnit(IDPlayer player, IPosition position, ITile tile)
        {
            return new UnitElf(player, position, tile);
        }
    }
}
