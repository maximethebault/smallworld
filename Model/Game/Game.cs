using System;
using System.Collections.Generic;
using System.Linq;
using Model.Difficulty;
using Model.Fight;
using Model.Fight.Exception;
using Model.Fight.Strategy;
using Model.Map;
using Model.Move;
using Model.Player;
using Model.Unit;
using Model.Unit.Exception;
using Model.Utils;

namespace Model.Game
{
    [Serializable()]
    public class Game : PropertyChangedNotifier, IDGame
    {
        public int ElapsedTurns { get; private set; }

        private bool _finished;

        public bool Finished
        {
            get { return _finished; }
            private set
            {
                _finished = value;
                RaisePropertyChanged("Finished");
            }
        }

        public IEnumerator<IDPlayer> PlayerTurnOrder { get; set; }

        public IPlayer CurrentPlayer
        {
            get { return PlayerTurnOrder.Current; }
        }

        private IPlayer _winner;
        public IPlayer Winner
        {
            get { return _winner; }
            set
            {
                _winner = value;
                RaisePropertyChanged("Winner");
            }
        }

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

        public IDifficultyStrategy DifficultyStrategy { get; set; }

        public Game()
        {
            ElapsedTurns = 0;
            Finished = false;
            IDFight = null;
        }

        private List<IDUnit> IDUnitsAt(IPosition position)
        {
            return IDPlayers.Select(player => player.IDUnitsAt(position)).SingleOrDefault(units => units.Count > 0) ?? new List<IDUnit>();
        }

        public List<IUnit> UnitsAt(IPosition position)
        {
            return Players.Select(player => player.UnitsAt(position)).SingleOrDefault(units => units.Count > 0) ?? new List<IUnit>();
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
            var isEnnemyOnTargetTile = unitsOnTile != null && unitsOnTile.Count > 0 && !unitsOnTile.First().IDPlayer.Equals(targetPlayer);
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

        public IUnit NextFightRound()
        {
            if (IDFight == null)
            {
                throw new NoFightException("No fight in progress!");
            }
            var roundWinner = IDFight.NextRound();
            if (!IDFight.Finished) return roundWinner;
            // if IDLoser is filled, someone was killed
            if (IDFight.IDLoser != null && IDFight.IDWinner != null && ReferenceEquals(IDFight.IDAttacker, IDFight.IDWinner))
            {
                IDFight.IDWinner.Kill(IDFight.IDLoser);
                IDFight.IDLoser.IDPlayer.IDUnits.Remove(IDFight.IDLoser);
                var unitsOnTile = IDUnitsAt(IDFight.IDLoser.Position).Count;
                if (unitsOnTile == 0)
                {
                    IDFight.IDWinner.MoveTo(IDFight.IDLoser.Position, IDFight.IDLoser.Tile);
                }
                else
                {
                    IDFight.IDWinner.SimulateMoveTo(IDFight.IDLoser.Tile);
                }
                CheckGameEnd();
            }
            else if (IDFight.IDAttacker != null)
            {
                IDFight.IDAttacker.SimulateMoveTo(IDFight.IDDefender.Tile);
            }
            IDFight = null;
            return roundWinner;
        }

        private bool CheckGameEnd()
        {
            var nbDead = IDPlayers.Count(player => !player.HasUnitLeft());

            if (nbDead < IDPlayers.Count - 1 && !DifficultyStrategy.IsMaxTurnNumberReached(ElapsedTurns)) return false;

            Finished = true;
            if (nbDead >= IDPlayers.Count - 1)
            {
                Winner = IDPlayers.FirstOrDefault(player => player.HasUnitLeft());
            }
            else
            {
                ComputeScore();
                var ordered = IDPlayers.OrderBy(player => player.Score);
                var greatest = ordered.LastOrDefault();
                var almostGreatest = ordered.ElementAtOrDefault(ordered.Count()-2);
                if (greatest == null || almostGreatest == null || greatest.Score != almostGreatest.Score)
                {
                    Winner = greatest;
                }
            }
            return true;
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

        public void PropelGame()
        {
            if (IDFight != null)
            {
                throw new FightInProgressException("Cannot finish turn because a fight is in progress!");
            }
            if (PlayerTurnOrder.MoveNext())
            {
                return;
            }
            if (CheckGameEnd())
            {
                return;
            }
            NextTurn();
        }

        public void ComputeScore()
        {
            foreach (var player in IDPlayers)
            {
                player.ComputeScore();
            }
        }

        private void NextTurn()
        {
            ElapsedTurns++;
            // let's reset the cursor to the first Player
            PlayerTurnOrder.Reset();
            PlayerTurnOrder.MoveNext();
            foreach (var player in IDPlayers)
            {
                player.ComputeScore();
                foreach (var unit in player.IDUnits)
                {
                    unit.ResetMovePoint();
                }
            }
        }
    }
}