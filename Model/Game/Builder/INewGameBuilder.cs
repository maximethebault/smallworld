using Model.Difficulty;

namespace Model.Game.Builder
{
    public interface INewGameBuilder : IGameBuilder
    {
        IDifficultyStrategy Difficulty { set; }

        void AddPlayer(string name, int race);
    }
}