using ClassLibrary.Difficulty;

namespace ClassLibrary.GameEngine.Builder
{
    public interface INewGameBuilder : IGameBuilder
    {

        void SetDifficulty(IDifficultyStrategy value);

        void AddPlayer(string name, Race.IDRace race);
    }
}