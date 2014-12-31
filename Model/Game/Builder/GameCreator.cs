namespace Model.Game.Builder
{
    public class GameCreator
    {
        private GameBuilder Builder
        {
            get;
            set;
        }

        public GameCreator(IGameBuilder gameBuilder)
        {
            Builder = (GameBuilder)gameBuilder;
        }

        public GameCreator CreateGame()
        {
            Builder.Create();
            Builder.BuildDifficulty();
            Builder.BuildMap();
            Builder.BuildPlayer();
            Builder.BuildUnits();
            return this;
        }

        public IGame GetGame()
        {
            return Builder.Game;
        }
    }
}