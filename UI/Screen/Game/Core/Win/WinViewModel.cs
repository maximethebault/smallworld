using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Model.Game;

namespace UI.Screen.Game.Core.Win
{
    class WinViewModel : ViewModelBase
    {
        public IGame Game { get; set; }

        #region Command
        public ICommand ExitGameCommand { get; private set; }
        #endregion Command

        #region Event
        public event GameExitEventHandler OnGameExit;
        public delegate void GameExitEventHandler(WinViewModel t, EventArgs e);
        #endregion Event

        public WinViewModel(IGame game)
        {
            Game = game;
            ExitGameCommand = new DelegateCommand(o => ExitGame());
        }

        private void ExitGame()
        {
            if (OnGameExit != null)
            {
                OnGameExit(this, null);
            }
        }
    }
}
