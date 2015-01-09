using Model.Player;

namespace Model.Turn
{
    public interface IDPlayerTurn
    {
        IPlayer CurrentPlayer { get; }
    }
}