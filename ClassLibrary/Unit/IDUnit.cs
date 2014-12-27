﻿using System;
using ClassLibrary.Map;
using ClassLibrary.Player;

namespace ClassLibrary.Unit
{
    public interface IDUnit : IUnit
    {
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
        void MoveTo(IPosition targetPosition, Tile.Tile targetTile);

        int GetScore();
        void Kill(Unit killed);

        /// <summary>
        /// Performs the following checks on the given movement :
        /// - Whether the unit has enough move point for this movement
        /// - Whether the movement is possible, geographically wise
        /// </summary>
        /// <param name="targetPosition">The target position we'd like to probe</param>
        /// <param name="targetTile">The target tile we'd like to probe</param>
        /// <param name="ennemyOnTargetTile">Whether the target tile is occupied by an ennemy</param>
        /// <returns>A boolean indicating whether the movement is possible</returns>
        bool CanMoveTo(IPosition targetPosition, Tile.Tile targetTile, bool ennemyOnTargetTile);

        void ComputeRoundWinner(Unit attackee);
        void ResetMovePoint();

        /// <summary>
        /// Removes a life point from 
        /// </summary>
        void DecrementLifePoint();
    }
}