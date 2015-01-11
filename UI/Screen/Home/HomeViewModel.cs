using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Model.Difficulty;
using Model.Game.Builder;
using UI.Screen.Game;

namespace UI.Screen.Home
{
    class HomeViewModel : ViewModelBase
    {
        public event NewGameEventHandler OnNewGame;
        public delegate void NewGameEventHandler(HomeViewModel i, EventArgs e);

        public event LoadGameEventHandler OnLoadGame;
        public delegate void LoadGameEventHandler(HomeViewModel i, GameEventArgs e);

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
            var dlg = new Microsoft.Win32.OpenFileDialog()
            {
                // Default file name
                FileName = "smallworld",
                // Default file extension
                DefaultExt = ".sws",
                // Filter files by extension
                Filter = "Small World game save|*.sws"
            };

            // Show save file dialog box
            var result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result != true) return;

            var filename = dlg.FileName;

            // Load document
            LoadGameFrom(filename);
        }

        private void LoadGameFrom(string filename)
        {
            var builder = BuilderFactory.GetLoadGameBuilder();
            builder.SaveFilepath = filename;
            var creator = BuilderFactory.GetGameCreator(builder);
            var game = creator.CreateGame().GetGame();
            if (OnLoadGame != null)
            {
                OnLoadGame(this, new GameEventArgs(game));
            }
        }
    }
}
