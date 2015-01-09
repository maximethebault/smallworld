using System;
using Model.Map;
using Model.Player;
using Model.Tile;

namespace Model.Unit
{
    class UnitElf : Unit
    {
        public UnitElf(IDPlayer player, IPosition position, ITile tile)
            : base(player, position, tile)
        {
        }

        public override void DecrementLifePoint()
        {
            if (HealthPoint == 1)
            {
                // a bonus allows an elf to survive once out of twice when it runs out of life points
                var rnd = new Random();
                if (rnd.Next(0, 2) == 0)
                {
                    // day of luck: we cancel the hp loss
                    return;
                }
            }
            HealthPoint--;
        }

        protected override float GetNeededPointToMoveAt(ITile targetTile)
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