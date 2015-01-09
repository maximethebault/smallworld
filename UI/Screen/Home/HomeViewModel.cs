using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace UI.Screen.Home
{
    class HomeViewModel : ViewModelBase
    {
        public event NewGameEventHandler OnNewGame;
        public delegate void NewGameEventHandler(HomeViewModel i, EventArgs e);

        public event LoadGameEventHandler OnLoadGame;
        public delegate void LoadGameEventHandler(HomeViewModel i, EventArgs e);

        public ICommand NewGameCommand { get; private set; }
        public ICommand LoadGameCommand { get; private set; }

        public HomeViewModel()
        {
            NewGameCommand = new DelegateCommand(o => NewGame());
            LoadGameCommand = new DelegateCommand(o => LoadGame());
        }

        private void NewGame()
        {
            if (OnNewGame != null)
            {
                OnNewGame(this, null);
            }
        }

        private void LoadGame()
        {
            if (OnLoadGame != null)
            {
                OnLoadGame(this, null);
            }
        }
    }
}
