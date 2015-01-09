using System;
using Model.Game;

namespace UI.Screen.Game
{

    public class GameEventArgs : EventArgs
    {
        public IGame Game { get; set; }

        public GameEventArgs(IGame game)
        {
            Game = game;
        }
    }
}
