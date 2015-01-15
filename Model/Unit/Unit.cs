using System;
using System.Collections.Generic;
using System.Linq;
using Model.Map;
using Model.Player;
using Model.Tile;
using Model.Utils;

namespace Model.Unit
{
    [Serializable()]
    abstract class Unit : PropertyChangedNotifier, IDUnit
    {
        protected virtual int DefaultScore
        {
            get { return 1; }
        }

        protected virtual float DefaultMovementCost
        {
            get { return 1; }
        }

        private int _healthPoint;

        public virtual int InitialHealthPoint
        {
            get { return 5; }
        }
        public int HealthPoint
        {
            get { return _healthPoint; }
            set
            {
                _healthPoint = value;
                RaisePropertyChanged("HealthPoint");
                RaisePropertyChanged("AttackPoint");
                RaisePropertyChanged("DefensePoint");
            }
        }

        public virtual int InitialAttackPoint
        {
            get { return 2; }
        }
        public float AttackPoint
        {
            get
            {
                var healthLeft = (float) HealthPoint / InitialHealthPoint;
                return InitialAttackPoint * healthLeft;
            }
        }

        public virtual int InitialDefensePoint
        {
            get { return 1; }
        }
        public float DefensePoint
        {
            get
            {
                var healthLeft = (float)HealthPoint / InitialHealthPoint;
                return InitialDefensePoint * healthLeft;
            }
        }

        public virtual float InitialMovePoint
        {
            get { return 1; }
        }
        private float _movePoint;
        public float MovePoint
        {
            get { return _movePoint; }
            set
            {
                _movePoint = value;
                RaisePropertyChanged("MovePoint");
            }
        }

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
            HealthPoint = InitialHealthPoint;
            MovePoint = InitialMovePoint;
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

        public void SimulateMoveTo(ITile targetTile)
        {
            MovePoint -= GetNeededPointToMoveAt(targetTile);
        }

        public virtual int ComputeScore()
        {
            return DefaultScore;
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
            MovePoint = InitialMovePoint;
        }

        public virtual void DecrementLifePoint()
        {
            HealthPoint--;
        }

        protected virtual bool IsMovementPossible(IPosition targetPosition, ITile targetTile, bool ennemyOnTargetTile)
        {
            return Position.IsAdjacent(targetPosition);
        }

        protected virtual float GetNeededPointToMoveAt(ITile targetTile)
        {
            return DefaultMovementCost;
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
