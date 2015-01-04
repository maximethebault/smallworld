using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Model.Difficulty;
using Model.Game.Builder;
using UI.Screen.Game.Creation.ViewModel;

namespace UI.Screen.Game.Creation
{
    /// <summary>
    /// Logique d'interaction pour GameCreation.xaml
    /// </summary>
    public partial class GameCreation : UserControl
    {
        public event BackHomeEventHandler OnBackHome;
        public delegate void BackHomeEventHandler(GameCreation i, EventArgs e);

        public event NewGameEventHandler OnNewGame;
        public delegate void NewGameEventHandler(GameCreation i, GameEventArgs e);

        public GameConfiguration Configuration { get; private set; }

        public GameCreation(String[] maps, String[] races, int playerCount)
        {
            Configuration = new GameConfiguration(maps, races, playerCount);
            InitializeComponent();
        }

        private void OnRaceSelection(object sender, SelectionChangedEventArgs e)
        {
            Configuration.OnRaceSelection(sender, e);
        }

        private void Back_Home(object sender, RoutedEventArgs e)
        {
            if (OnBackHome != null)
            {
                OnBackHome(this, null);
            }
        }

        private void Start_Game(object sender, RoutedEventArgs e)
        {
            if (OnNewGame == null)
            {
                return;
            }
            var game = Configuration.BuildGame();
            if (game != null)
            {
                OnNewGame(this, new GameEventArgs(game));
            }
        }
    }
}
