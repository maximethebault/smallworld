using System.Collections.Generic;
using ClassLibrary.Map;
using ClassLibrary.Player;
using ClassLibrary.Turn;
using ClassLibrary.Unit;

namespace ClassLibrary.GameEngine
{
    public interface IDGame : IGame
    {
        List<IDPlayer> IDPlayers { get; set; }
        IDMap IDMap { get; set; }
    }
}