using System;
using System.Collections.Generic;
using System.Linq;
using Model.Game.Exception;
using Model.Difficulty;
using Model.Fight;
using Model.Fight.Exception;
using Model.Fight.Strategy;
using Model.Map;
using Model.Move;
using Model.Player;
using Model.Race;
using Model.Turn;
using Model.Unit;
using Model.Unit.Exception;

namespace Model.Game
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
                IDFight = new Fight.Fight(targetUnit, Unit.Unit.GetBestUnit(unitsOnTile), new RandomFightStrategy());
                move.Fight = true;
            }
            else
            {
                targetUnit.MoveTo(targetPosition, targetTile);
                move.Success = true;
            }
            return move;
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

        public void NextFightRound()
        {
            if (IDFight == null)
            {
                throw new NoFightException("No fight in progress!");
            }
            IDFight.NextRound();
            if (!IDFight.IsFinished()) return;
            if (IDFight.Loser != null)
            {
                foreach (var player in IDPlayers)
                {
                    player.IDUnits.RemoveAll(IDFight.Loser.Equals);
                }
                CheckGameEnd();
            }
            IDFight = null;
        }

        private void CheckGameEnd()
        {
            var nbDead = IDPlayers.Count(player => player.HasUnitLeft());
            if (nbDead >= IDPlayers.Count - 1)
            {
                throw new NotImplementedException();
            }
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