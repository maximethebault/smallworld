using ClassLibrary.Map;
using ClassLibrary.Player;

namespace ClassLibrary.Unit
{
    public class UnitOrc : Unit
    {
        private int _bonusPoint;

        public UnitOrc(IDPlayer player, IPosition position, Tile.Tile tile) : base(player, position, tile)
        {
            _bonusPoint = 0;
        }

        public override void Kill(Unit killed)
        {
            _bonusPoint++;
            base.Kill(killed);
        }

        public override int GetScore()
        {
            if (Tile.IsForest())
                return 0;
            return _bonusPoint + base.GetScore();
        }

        protected override float GetNeededPointToMoveAt(Tile.Tile targetTile)
        {
            if (targetTile.IsPlain())
            {
                return UnitDefaultMovementCost / 2;
            }
            return base.GetNeededPointToMoveAt(targetTile);
        }
    }
}