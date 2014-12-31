using Model.Player;

namespace Model.Turn
{
    public interface IDPlayerTurn
    {
        IDUnitTurn CurrentIDUnitTurn { get; }
        IPlayer CurrentPlayer { get; }
    }
}