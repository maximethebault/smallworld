using Model.Map;
using Model.Player;
using Model.Tile;

namespace Model.Unit
{
    public interface IDUnit : IUnit
    {
        new int HealthPoint { get; set; }
        new float MovePoint { get; set; }
        new IPosition Position { get; set; }
        ITile Tile { get; set; }
        IDPlayer IDPlayer { get; set; }

        /// <summary>
        /// Checks if a unit is dead
        /// </summary>
        /// <returns>Whether the unit is dead</returns>
        bool IsDead();

        /// <summary>
        /// Blindly moves the unit to the given position (doesn't do any consistency check!)
        /// </summary>
        /// <param name="targetPosition">The target position</param>
        /// <param name="targetTile">The target tile</param>
        void MoveTo(IPosition targetPosition, ITile targetTile);

        int ComputeScore();
        void Kill(IDUnit killed);

        /// <summary>
        /// Performs the following checks on the given movement :
        /// - Whether the unit has enough move point for this movement
        /// - Whether the movement is possible, geographically wise
        /// </summary>
        /// <param name="targetPosition">The target position we'd like to probe</param>
        /// <param name="targetTile">The target tile we'd like to probe</param>
        /// <param name="ennemyOnTargetTile">Whether the target tile is occupied by an ennemy</param>
        /// <returns>A boolean indicating whether the movement is possible</returns>
        bool CanMoveTo(IPosition targetPosition, ITile targetTile, bool ennemyOnTargetTile);

        void ResetMovePoint();

        /// <summary>
        /// Removes a life point from 
        /// </summary>
        void DecrementLifePoint();
    }
}