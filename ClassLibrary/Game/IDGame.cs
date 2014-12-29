using System.Collections.Generic;
using ClassLibrary.Map;
using ClassLibrary.Move.Fight;
using ClassLibrary.Player;
using ClassLibrary.Turn;

namespace ClassLibrary.Game
{
    public interface IDGame : IGame
    {
        List<IDPlayer> IDPlayers { get; set; }
        IDMap IDMap { get; set; }
        IDPlayerTurn CurrentIDPlayerTurn { get; }
        IDFight IDFight { get; set; }
    }
}