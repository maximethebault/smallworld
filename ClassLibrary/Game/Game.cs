using System;
using System.Collections.Generic;
using System.Linq;
using ClassLibrary.Difficulty;
using ClassLibrary.Game.Exception;
using ClassLibrary.Map;
using ClassLibrary.Move;
using ClassLibrary.Move.Fight;
using ClassLibrary.Player;
using ClassLibrary.Race;
using ClassLibrary.Turn;
using ClassLibrary.Unit;
using ClassLibrary.Unit.Exception;

namespace ClassLibrary.Game
{
    public class Game : IDGame
    {
        private int elapsedTurns;
        private IEnumerable<IDPlayer> playerTurnOrder;

        public List<IDPlayer> IDPlayers { get; set; }

        public List<IPlayer> Players
        {
            get { return IDPlayers.Cast<IPlayer>().ToList(); }
        }

        public IDMap IDMap { get; set; }

        public IMap Map
        {
            get { return IDMap; }
        }

        public IDFight IDFight { get; set; }

        public IFight Fight
        {
            get { return IDFight; }
        }

        public IDPlayerTurn CurrentIDPlayerTurn { get; set; }

        public IDifficultyStrategy DifficultyStrategy { get; set; }

        public Game()
        {
            IDFight = null;
        }

        public IMove MoveUnit(IUnit movedUnit, IPosition targetPosition)
        {
            if (IDFight != null)
            {
                throw new FightInProgressException("Cannot move unit because a fight is in progress!");
            }
            IDPlayer targetPlayer = null;
            IDUnit targetUnit = null;
            foreach (var player in IDPlayers)
            {
                targetUnit = player.IDUnits.FirstOrDefault(movedUnit.Equals);
                targetPlayer = player;
                if (targetUnit != null)
                {
                    break;
                }
            }
            if (targetUnit == null)
            {
                throw new UnitNotFoundException("The given unit doesn't belong to any player, and thus can't be moved.");
            }
            var targetTile = Map.TileAtPosition(targetPosition);
            var unitsOnTile = IDUnitsAt(targetPosition);
            var isEnnemyOnTargetTile = unitsOnTile != null && !unitsOnTile.First().IDPlayer.Equals(targetPlayer);
            if (!targetUnit.CanMoveTo(targetPosition, targetTile, isEnnemyOnTargetTile))
            {
                throw new UnitMovementUnauthorized("The given unit can't be moved to the given position.");
            }
            var move = new Move.Move();
            if (isEnnemyOnTargetTile)
            {
                IDFight = new Fight(targetUnit, Unit.Unit.GetBestUnit(unitsOnTile));
                move.Fight = true;
            }
            else
            {
                move.Success = true;
            }
            return move;
        }

        private void KillUnit(IDUnit killer, IDUnit killed)
        {
            throw new NotImplementedException();
        }

        public void FinishUnitTurn()
        {
            if (IDFight != null)
            {
                throw new FightInProgressException("Cannot finish turn because a fight is in progress!");
            }
            throw new NotImplementedException();
        }

        public void FinishPlayerTurn()
        {
            if (IDFight != null)
            {
                throw new FightInProgressException("Cannot finish turn because a fight is in progress!");
            }
            throw new NotImplementedException();
        }

        public void FinishFightRound()
        {
            if (IDFight == null)
            {
                throw new NoFightException("No fight in progress!");
            }
            throw new NotImplementedException();
        }

        private void CheckGameEnd()
        {
            throw new NotImplementedException();
        }

        public bool CanMoveUnit(IUnit movedUnit, IPosition targetPosition)
        {
            IDPlayer targetPlayer = null;
            IDUnit targetUnit = null;
            foreach (var player in IDPlayers)
            {
                targetUnit = player.IDUnits.FirstOrDefault(movedUnit.Equals);
                targetPlayer = player;
                if (targetUnit != null)
                {
                    break;
                }
            }
            if (targetUnit == null)
            {
                throw new UnitNotFoundException("The given unit doesn't belong to any player, and thus can't be moved.");
            }
            var targetTile = Map.TileAtPosition(targetPosition);
            var unitsOnTile = IDUnitsAt(targetPosition);
            var isEnnemyOnTargetTile = unitsOnTile != null && !unitsOnTile.First().IDPlayer.Equals(targetPlayer);
            return targetUnit.CanMoveTo(targetPosition, targetTile, isEnnemyOnTargetTile);
        }

        private IRace GetRaceTypeAt(IPosition position)
        {
            throw new NotImplementedException();
        }

        private void StartNewTurn()
        {
            throw new NotImplementedException();
        }

        private List<IDUnit> IDUnitsAt(IPosition position)
        {
            return IDPlayers.Select(player => player.IDUnitsAt(position)).FirstOrDefault(playerUnits => playerUnits.Count > 0);
        }

        public List<IUnit> UnitsAt(IPosition position)
        {
            return Players.Select(player => player.UnitsAt(position)).FirstOrDefault(playerUnits => playerUnits.Count > 0);
        }

        public void StartGame()
        {
            throw new NotImplementedException();
        }
    }
}