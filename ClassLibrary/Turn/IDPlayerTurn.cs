using ClassLibrary.Player;

namespace ClassLibrary.Turn
{
    public interface IDPlayerTurn
    {
        IDUnitTurn CurrentIDUnitTurn { get; }
        IPlayer CurrentPlayer { get; }
    }
}