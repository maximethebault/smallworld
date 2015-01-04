namespace Model.Game.Builder
{
    public interface IGameCreator
    {
        IGameCreator CreateGame();
        IGame GetGame();
    }
}