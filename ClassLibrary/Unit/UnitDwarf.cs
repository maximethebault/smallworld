﻿using ClassLibrary.Map;
using ClassLibrary.Player;
using ClassLibrary.Tile;

namespace ClassLibrary.Unit
{
    public class UnitDwarf : Unit
    {
        public UnitDwarf(IDPlayer player, IPosition position, ITile tile)
            : base(player, position, tile)
        {
        }

        protected override bool IsMovementPossible(IPosition targetPosition, ITile targetTile, bool ennemyOnTargetTile)
        {
            if (Tile.IsMountain() && targetTile.IsMountain() && !ennemyOnTargetTile)
                return true;
            return base.IsMovementPossible(targetPosition, targetTile, ennemyOnTargetTile);
        }

        protected override float GetNeededPointToMoveAt(ITile targetTile)
        {
            if (targetTile.IsPlain())
            {
                return UnitDefaultMovementCost / 2;
            }
            return base.GetNeededPointToMoveAt(targetTile);
        }

        public override int GetScore()
        {
            return Tile.IsPlain() ? 0 : base.GetScore();
        }
    }
}