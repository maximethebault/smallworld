using Model.Difficulty;

namespace Model.Game.Builder
{
    public interface INewGameBuilder : IGameBuilder
    {

        void SetDifficulty(IDifficultyStrategy value);

        void AddPlayer(string name, int race);
    }
}