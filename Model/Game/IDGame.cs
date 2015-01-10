using System.Collections.Generic;
using Model.Difficulty;
using Model.Fight;
using Model.Map;
using Model.Player;
using Model.Turn;

namespace Model.Game
{
    public interface IDGame : IGame
    {
        List<IDPlayer> IDPlayers { get; set; }
        IDMap IDMap { get; set; }
        IDPlayerTurn CurrentIDPlayerTurn { get; }
        IDFight IDFight { get; set; }
        new IDifficultyStrategy DifficultyStrategy { get; set; }
        IEnumerator<IDPlayer> PlayerTurnOrder { get; set; }
        void ComputeScore();
    }
}