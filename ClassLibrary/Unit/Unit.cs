using System;
using System.Collections.Generic;
using System.Linq;
using ClassLibrary.Map;
using ClassLibrary.Player;

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

        protected int _lifePoint;
        protected int _attackPoint;
        protected int _defensePoint;
        protected float _movePoint;

        public IPosition Position
        {
            get;
            set;
        }

        public Tile.Tile Tile
        {
            get;
            set;
        }

        public IDPlayer IDPlayer { get; set; }

        protected Unit(IDPlayer player, IPosition position, Tile.Tile tile)
        {
            IDPlayer = player;
            Position = position;
            Tile = tile;
            _lifePoint = UnitDefaultLifePoint;
            _attackPoint = UnitDefaultAttackPoint;
            _defensePoint = UnitDefaultDefensePoint;
            _movePoint = UnitDefaultMovePoint;
        }

        /// <summary>
        /// Checks if a unit is dead
        /// </summary>
        /// <returns>Whether the unit is dead</returns>
        public bool IsDead()
        {
            return _lifePoint <= 0;
        }

        /// <summary>
        /// Blindly moves the unit to the given position (doesn't do any consistency check!)
        /// </summary>
        /// <param name="targetPosition">The target position</param>
        /// <param name="targetTile">The target tile</param>
        public void MoveTo(IPosition targetPosition, Tile.Tile targetTile)
        {
            Position = targetPosition;
            Tile = targetTile;
            _movePoint -= GetNeededPointToMoveAt(targetTile);
        }

        public virtual int GetScore()
        {
            return UnitDefaultScore;
        }

        public virtual void Kill(Unit killed)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Performs the following checks on the given movement :
        /// - Whether the unit has enough move point for this movement
        /// - Whether the movement is possible, geographically wise
        /// </summary>
        /// <param name="targetPosition">The target position we'd like to probe</param>
        /// <param name="targetTile">The target tile we'd like to probe</param>
        /// <param name="ennemyOnTargetTile">Whether the target tile is occupied by an ennemy</param>
        /// <returns>A boolean indicating whether the movement is possible</returns>
        public bool CanMoveTo(IPosition targetPosition, Tile.Tile targetTile, bool ennemyOnTargetTile)
        {
            return IsMovementPossible(targetPosition, targetTile, ennemyOnTargetTile) && GetNeededPointToMoveAt(targetTile) <= _movePoint;
        }

        public void ComputeRoundWinner(Unit attackee)
        {
            throw new NotImplementedException();
        }

        public static Unit GetBestUnit(IEnumerable<Unit> units)
        {
            return units.OrderBy(unit => unit._lifePoint).Last();
        }

        public void ResetMovePoint()
        {
            _movePoint = UnitDefaultMovePoint;
        }

        /// <summary>
        /// Removes a life point from 
        /// </summary>
        public virtual void DecrementLifePoint()
        {
            _lifePoint--;
            // TODO: something else? check dead?
        }

        /// <summary>
        /// Whether the movement is possible, geographically wise
        /// </summary>
        /// <param name="targetPosition">The target position we'd like to probe</param>
        /// <param name="targetTile">The target tile we'd like to probe</param>
        /// <param name="ennemyOnTargetTile">Whether the target tile is occupied by an ennemy</param>
        /// <returns>A boolean indicating whether the movement is possible, geographically wise</returns>
        protected virtual bool IsMovementPossible(IPosition targetPosition, Tile.Tile targetTile, bool ennemyOnTargetTile)
        {
            return Position.IsAdjacent(targetPosition);
        }

        /// <summary>
        /// Get the number of move point needed for this movement
        /// </summary>
        /// <param name="targetTile">The target tile we'd like to probe</param>
        /// <returns>The number of move point needed for this movement</returns>
        protected virtual float GetNeededPointToMoveAt(Tile.Tile targetTile)
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
