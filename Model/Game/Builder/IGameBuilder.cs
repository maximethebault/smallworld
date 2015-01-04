namespace Model.Game.Builder
{
    public interface IGameBuilder
    {
        IGame Game { get; }

        void Create();
        void BuildPlayer();
        void BuildUnits();
        void BuildMap();
        void BuildDifficulty();
    }
}