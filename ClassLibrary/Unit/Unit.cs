using System;
using System.Collections.Generic;
using System.Linq;
using ClassLibrary.Map;
using ClassLibrary.Player;
using ClassLibrary.Tile;

namespace ClassLibrary.Unit
{
    abstract public class Unit : IDUnit
    {
        // as we want to make the following constants overridable, we make them static
        protected static float UnitDefaultMovementCost = 1;
        protected static int UnitDefaultScore = 1;
        protected static int UnitDefaultLifePoint = 1;
        protected static int UnitDefaultAttackPoint = 1;
        protected static int UnitDefaultDefensePoint = 1;
        protected static float UnitDefaultMovePoint = 1;

        public int HealthPoint { get; set; }
        public int AttackPoint { get; set; }
        public int DefensePoint { get; set; }
        public float MovePoint { get; set; }

        public IPosition Position { get; set; }

        public ITile Tile { get; set; }

        public IDPlayer IDPlayer { get; set; }

        protected Unit(IDPlayer player, IPosition position, ITile tile)
        {
            IDPlayer = player;
            Position = position;
            Tile = tile;
            HealthPoint = UnitDefaultLifePoint;
            AttackPoint = UnitDefaultAttackPoint;
            DefensePoint = UnitDefaultDefensePoint;
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

        public virtual void Kill(Unit killed)
        {
            throw new NotImplementedException();
        }

        public bool CanMoveTo(IPosition targetPosition, ITile targetTile, bool ennemyOnTargetTile)
        {
            return IsMovementPossible(targetPosition, targetTile, ennemyOnTargetTile) && GetNeededPointToMoveAt(targetTile) <= MovePoint;
        }

        public void ComputeRoundWinner(Unit attackee)
        {
            throw new NotImplementedException();
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
            return (float) UnitDefaultMovementCost;
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
            throw new NotImplementedException();
        }
    }
}
