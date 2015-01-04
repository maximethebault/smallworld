using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
