using System.Collections.Generic;
using Model.Difficulty;
using Model.Fight;
using Model.Map;
using Model.Move;
using Model.Player;
using Model.Unit;

namespace Model.Game
{
    public interface IGame
    {
        List<IPlayer> Players { get; }
        IMap Map { get; }
        IFight Fight { get; }
        IDifficultyStrategy DifficultyStrategy { get; }
        int CurrentTurn { get; }
        IPlayer CurrentPlayer { get; }
        bool Finished { get; }
        IPlayer Winner { get; set; }

        bool CanMoveUnit(IUnit unit, IPosition targetPosition);
        IMove MoveUnit(IUnit unit, IPosition targetPosition);
        void PropelGame();
        IUnit NextFightRound();
        IEnumerable<IUnit> UnitsAt(IPosition position);
        unsafe List<IPosition> ComputeAdvices(IUnit unit);
    }
}