namespace ClassLibrary.GameEngine.Builder
{
    abstract public class GameBuilder : IGameBuilder
    {
        public Game Game
        {
            get;
            private set;
        }

        public virtual void Create()
        {
            Game = new Game();
        }

        public abstract void BuildPlayer();

        public abstract void BuildUnits();

        public abstract void BuildMap();

        public abstract void BuildDifficulty();
    }
}
