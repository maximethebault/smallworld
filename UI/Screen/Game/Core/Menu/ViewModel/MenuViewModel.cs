using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Model.Game;

namespace UI.Screen.Game.Core.Menu.ViewModel
{
    class MenuViewModel : ViewModelBase
    {
        #region Properties
        public IGame Game { get; private set; }

        private bool _visible;
        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                RaisePropertyChanged("Visible");
            }
        }
        #endregion Properties

        #region Command
        public ICommand ResumeGameCommand { get; private set; }
        public ICommand SaveGameCommand { get; private set; }
        public ICommand ExitGameCommand { get; private set; }
        #endregion Command

        #region Event
        public event GameExitEventHandler OnGameExit;
        public delegate void GameExitEventHandler(MenuViewModel t, EventArgs e);
        #endregion Event

        public MenuViewModel(IGame game)
        {
            Game = game;
            Visible = false;

            ResumeGameCommand = new DelegateCommand(o => ResumeGame());
            SaveGameCommand = new DelegateCommand(o => SaveGame());
            ExitGameCommand = new DelegateCommand(o => ExitGame());
        }

        private void ResumeGame()
        {
            Visible = false;
        }

        private void SaveGame()
        {
            var dlg = new Microsoft.Win32.SaveFileDialog
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

            // Save document
            SaveGameTo(filename);
        }

        private void SaveGameTo(String path)
        {
            Stream stream = File.Open(path, FileMode.Create);
            var bformatter = new BinaryFormatter();

            bformatter.Serialize(stream, Game);
            stream.Close();
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
