using ClassLibrary.Player;

namespace ClassLibrary.Turn
{
    public interface IPlayerTurn
    {
        IUnitTurn GetCurrentUnitTurn();
        IPlayer GetCurrentPlayer();
    }
}