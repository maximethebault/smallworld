namespace ClassLibrary.Game.Builder
{
    abstract public class GameBuilder : IGameBuilder
    {
        public ClassLibrary.Game.Game Game
        {
            get;
            private set;
        }

        public virtual void Create()
        {
            Game = new ClassLibrary.Game.Game();
        }

        public abstract void BuildPlayer();

        public abstract void BuildUnits();

        public abstract void BuildMap();

        public abstract void BuildDifficulty();
    }
}
