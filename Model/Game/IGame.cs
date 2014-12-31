using System.Collections.Generic;
using Model.Turn;
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
        bool CanMoveUnit(IUnit unit, IPosition targetPosition);
        IMove MoveUnit(IUnit unit, IPosition targetPosition);
        void FinishUnitTurn();
        void FinishPlayerTurn();
        void NextFightRound();
        List<IUnit> UnitsAt(IPosition position);
        void StartGame();
    }
}