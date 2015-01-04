namespace Model.Game.Builder
{
    class GameCreator : IGameCreator
    {
        private IGameBuilder Builder
        {
            get;
            set;
        }

        public GameCreator(IGameBuilder gameBuilder)
        {
            Builder = gameBuilder;
        }

        public IGameCreator CreateGame()
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