using System.Collections.Generic;
using ClassLibrary.Map;
using ClassLibrary.Player;
using ClassLibrary.Turn;
using ClassLibrary.Unit;

namespace ClassLibrary.GameEngine
{
    public interface IGame
    {
        List<IPlayer> GetPlayers();
        IMap GetMap();
        IPlayerTurn GetCurrentPlayerTurn();
        void MoveUnit(IUnit unit, IPosition targetPosition);
        void FinishUnitTurn();
        void FinishPlayerTurn();
        bool CanMoveUnit(IUnit unit, IPosition targetPosition);
        List<IUnit> GetUnitsAt(IPosition position);
        void StartGame();
    }
}