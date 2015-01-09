using Model.Map;
using Model.Player;
using Model.Tile;

namespace Model.Unit
{
    class UnitDwarf : Unit
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

        public override int ComputeScore()
        {
            return Tile.IsPlain() ? 0 : base.ComputeScore();
        }
    }
}