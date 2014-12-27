using System;
using System.Collections.Generic;
using System.Linq;
using ClassLibrary.Difficulty;
using ClassLibrary.Map;
using ClassLibrary.Player;
using ClassLibrary.Race;
using ClassLibrary.Turn;
using ClassLibrary.Unit;
using ClassLibrary.Unit.Exception;

namespace ClassLibrary.GameEngine
{
    public class Game : IGame
    {
        private int elapsedTurns;
        private IEnumerable<IDPlayer> playerTurnOrder;

        public Game()
        {
            //throw new NotImplementedException();
        }

        public List<IDPlayer> Players
        {
            get;
            set;
        }

        public List<IPlayer> GetPlayers()
        {
            return Players.Cast<IPlayer>().ToList();
        }

        public Map.Map Map
        {
            get;
            set;
        }

        public IMap GetMap()
        {
            return Map;
        }

        public PlayerTurn CurrentPlayerTurn
        {
            get;
            set;
        }

        public IPlayerTurn GetCurrentPlayerTurn()
        {
            return CurrentPlayerTurn;
        }

        public IDifficultyStrategy DifficultyStrategy
        {
            get;
            set;
        }

        public void MoveUnit(IUnit movedUnit, IPosition targetPosition)
        {
            IDPlayer targetPlayer = null;
            IDUnit targetUnit = null;
            foreach (var player in Players)
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
            var targetTile = Map.TileMap[targetPosition.GetX(), targetPosition.GetY()];
            var unitsOnTile = this.GetIDUnitsAt(targetPosition);
            var isEnnemyOnTargetTile = unitsOnTile != null && !unitsOnTile.First().IDPlayer.Equals(targetPlayer);
            if (!targetUnit.CanMoveTo(targetPosition, targetTile, isEnnemyOnTargetTile))
            {
                throw new UnitMovementUnauthorized("The given unit can't be moved to the given position.");
            }
            if (isEnnemyOnTargetTile)
            {
                EngageUnit(targetUnit, Unit.Unit.GetBestUnit(unitsOnTile));
            }
        }

        private void KillUnit(IDUnit killer, IDUnit killed)
        {
            throw new NotImplementedException();
        }

        public void FinishUnitTurn()
        {
            throw new NotImplementedException();
        }

        public void FinishPlayerTurn()
        {
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
            foreach (var player in Players)
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
            var targetTile = Map.TileMap[targetPosition.GetX(), targetPosition.GetY()];
            var unitsOnTile = this.GetIDUnitsAt(targetPosition);
            var isEnnemyOnTargetTile = unitsOnTile != null && !unitsOnTile.First().IDPlayer.Equals(targetPlayer);
            return targetUnit.CanMoveTo(targetPosition, targetTile, isEnnemyOnTargetTile);
        }

        private IRace GetRaceTypeAt(IPosition position)
        {
            throw new NotImplementedException();
        }

        private void EngageUnit(Unit.Unit attacker, Unit.Unit attackee)
        {
            throw new NotImplementedException();
        }

        private void StartNewTurn()
        {
            throw new NotImplementedException();
        }

        private List<IDUnit> GetIDUnitsAt(IPosition position)
        {
            return Players.Select(player => player.IDUnitsAt(position)).FirstOrDefault(playerUnits => playerUnits.Count > 0);
        }

        public List<IUnit> GetUnitsAt(IPosition position)
        {
            return Players.Select(player => player.UnitsAt(position)).FirstOrDefault(playerUnits => playerUnits.Count > 0);
        }

        public void StartGame()
        {
            throw new NotImplementedException();
        }
    }
}