using System;
using ClassLibrary.Map;
using ClassLibrary.Player;

namespace ClassLibrary.Unit
{
    public class UnitElf : Unit
    {
        public UnitElf(IDPlayer player, IPosition position, Tile.Tile tile) : base(player, position, tile)
        {
        }

        public override void DecrementLifePoint()
        {
            if (_lifePoint == 1)
            {
                // a bonus allows an elf to survive once out of twice when it runs out of life points
                var rnd = new Random();
                if (rnd.Next(0, 2) == 0)
                {
                    // day of luck: we cancel the hp loss
                    return;
                }
            }
            _lifePoint--;
        }

        protected override float GetNeededPointToMoveAt(Tile.Tile targetTile)
        {
            if (targetTile.IsForest())
            {
                return UnitDefaultMovementCost/2;
            }
            if (targetTile.IsDesert())
            {
                return UnitDefaultMovementCost * 2;
            }
            return base.GetNeededPointToMoveAt(targetTile);
        }
    }
}