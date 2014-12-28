using ClassLibrary.Map;
using ClassLibrary.Player;
using ClassLibrary.Tile;

namespace ClassLibrary.Unit
{
    public class UnitOrc : Unit
    {
        private int _bonusPoint;

        public UnitOrc(IDPlayer player, IPosition position, ITile tile)
            : base(player, position, tile)
        {
            _bonusPoint = 0;
        }

        public override IDUnit ShallowCopy()
        {
            var copy = new UnitOrc(IDPlayer, Position, Tile) {_bonusPoint = _bonusPoint};
            ShallowCopyProperties(copy);
            return copy;
        }

        public override void Kill(Unit killed)
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