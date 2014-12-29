using System.Collections.Generic;
using ClassLibrary.Map;
using ClassLibrary.Move;
using ClassLibrary.Move.Fight;
using ClassLibrary.Player;
using ClassLibrary.Turn;
using ClassLibrary.Unit;

namespace ClassLibrary.Game
{
    public interface IGame
    {
        List<IPlayer> Players { get; }
        IMap Map { get; }
        IFight Fight { get; }
        bool CanMoveUnit(IUnit unit, IPosition targetPosition);
        IMove MoveUnit(IUnit unit, IPosition targetPosition);
        void FinishUnitTurn();
        void FinishPlayerTurn();
        void FinishFightRound();
        List<IUnit> UnitsAt(IPosition position);
        void StartGame();
    }
}