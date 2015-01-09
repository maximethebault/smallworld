using System.Collections.Generic;
using System.Linq;
using Model.Map;
using Model.Player;
using Model.Tile;

namespace Model.Unit
{
    abstract class Unit : IDUnit
    {
        // as we want to make the following constants overridable, we make them static
        public static float UnitDefaultMovementCost = 1;
        public static int UnitDefaultScore = 1;
        public static int UnitDefaultHealthPoint = 5;
        public static int UnitDefaultAttackPoint = 2;
        public static int UnitDefaultDefensePoint = 1;
        public static float UnitDefaultMovePoint = 1;

        public int HealthPoint { get; set; }

        public float AttackPoint
        {
            get
            {
                var healthLeft = (float) HealthPoint/UnitDefaultHealthPoint;
                return UnitDefaultAttackPoint * healthLeft;
            }
        }

        public float DefensePoint
        {
            get
            {
                var healthLeft = (float) HealthPoint / UnitDefaultHealthPoint;
                return UnitDefaultDefensePoint * healthLeft;
            }
        }

        public float MovePoint { get; set; }

        public IPosition Position { get; set; }

        public ITile Tile { get; set; }

        public IPlayer Player
        {
            get { return IDPlayer; }
        }

        public IDPlayer IDPlayer { get; set; }

        protected Unit(IDPlayer player, IPosition position, ITile tile)
        {
            IDPlayer = player;
            Position = position;
            Tile = tile;
            HealthPoint = UnitDefaultHealthPoint;
            MovePoint = UnitDefaultMovePoint;
        }

        public bool IsDead()
        {
            return HealthPoint <= 0;
        }

        public void MoveTo(IPosition targetPosition, ITile targetTile)
        {
            Position = targetPosition;
            Tile = targetTile;
            MovePoint -= GetNeededPointToMoveAt(targetTile);
        }

        public virtual int ComputeScore()
        {
            return UnitDefaultScore;
        }

        public virtual void Kill(IDUnit killed)
        {
        }

        public bool CanMoveTo(IPosition targetPosition, ITile targetTile, bool ennemyOnTargetTile)
        {
            return IsMovementPossible(targetPosition, targetTile, ennemyOnTargetTile) && GetNeededPointToMoveAt(targetTile) <= MovePoint;
        }

        public static IDUnit GetBestUnit(IEnumerable<IDUnit> units)
        {
            return units.OrderBy(unit => unit.HealthPoint).Last();
        }

        public void ResetMovePoint()
        {
            MovePoint = UnitDefaultMovePoint;
        }

        public virtual void DecrementLifePoint()
        {
            HealthPoint--;
            // TODO: something else? check dead?
        }

        protected virtual bool IsMovementPossible(IPosition targetPosition, ITile targetTile, bool ennemyOnTargetTile)
        {
            return Position.IsAdjacent(targetPosition);
        }

        protected virtual float GetNeededPointToMoveAt(ITile targetTile)
        {
            return UnitDefaultMovementCost;
        }

        public bool Equals(IUnit other)
        {
            return ReferenceEquals(this, other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as IUnit;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = HealthPoint;
                hashCode = (hashCode * 397) ^ MovePoint.GetHashCode();
                hashCode = (hashCode * 397) ^ (Position != null ? Position.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Tile != null ? Tile.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (IDPlayer != null ? IDPlayer.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
