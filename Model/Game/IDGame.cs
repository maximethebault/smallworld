using System.Collections.Generic;
using Model.Difficulty;
using Model.Fight;
using Model.Map;
using Model.Player;

namespace Model.Game
{
    public interface IDGame : IGame
    {
        List<IDPlayer> IDPlayers { get; set; }
        IDMap IDMap { get; set; }
        IDFight IDFight { get; set; }
        new IDifficultyStrategy DifficultyStrategy { get; set; }
        IEnumerator<IDPlayer> PlayerTurnOrder { get; set; }
        void ComputeScore();
    }
}