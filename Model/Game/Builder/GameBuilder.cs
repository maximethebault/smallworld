using Model.Difficulty;

namespace Model.Game.Builder
{
    abstract class GameBuilder : IGameBuilder
    {
        protected IDGame IDGame
        {
            get;
            set;
        }

        public IGame Game
        {
            get { return IDGame; }
        }

        public IDifficultyStrategy Difficulty
        {
            get;
            set;
        }

        public virtual void Create()
        {
            IDGame = new Game();
        }

        public abstract void BuildPlayer();

        public abstract void BuildUnits();

        public abstract void BuildMap();

        public abstract void BuildDifficulty();
    }
}
