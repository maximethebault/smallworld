using System;
using Model.Map;
using Model.Player;
using Model.Tile;

namespace Model.Unit
{
    [Serializable()]
    class UnitOrc : Unit
    {
        private int _bonusPoint;

        public UnitOrc(IDPlayer player, IPosition position, ITile tile)
            : base(player, position, tile)
        {
            _bonusPoint = 0;
        }

        public override void Kill(IDUnit killed)
        {
            _bonusPoint++;
            base.Kill(killed);
        }

        public override int ComputeScore()
        {
            if (Tile.IsForest())
                return 0;
            return _bonusPoint + base.ComputeScore();
        }

        protected override float GetNeededPointToMoveAt(ITile targetTile)
        {
            if (targetTile.IsPlain())
            {
                return UnitDefaultMovementCost / 2;
            }
            return base.GetNeededPointToMoveAt(targetTile);
        }
    }
}