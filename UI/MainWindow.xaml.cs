using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model.Game;
using UI.Screen.Game;
using UI.Screen.Game.Core;
using UI.Screen.Game.Creation;
using UI.Screen.Home;
using UI.Screen.Intro;

namespace UI
{
    /// <summary>
    /// CodeBehind for the main window
    /// Responsible for displaying the right application screen (role: router)
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            StartIntro();
        }

        private void StartIntro()
        {
            var intro = new Intro();
            intro.OnIntroEnd += OnIntroEnd;
            DataContext = intro;
        }

        private void StartHome()
        {
            var home = new Home();
            home.OnNewGame += OnNewGame;
            home.OnLoadGame += OnLoadGame;
            DataContext = home;
        }

        private void StartNewGame()
        {
            var maps = FindResource("Maps") as PathItem[];
            var races = FindResource("Races") as PathItem[];
            var playerCount = (int) FindResource("PlayerCount");
            var newGame = new GameCreation(maps, races, playerCount);
            newGame.OnNewGame += OnStartGame;
            newGame.OnBackHome += OnBackHome;
            DataContext = newGame;
        }

        private void StartLoadGame()
        {
            var loadGame = new Intro();
            loadGame.OnIntroEnd += OnIntroEnd;
            DataContext = loadGame;
        }

        private void StartGame(IGame game)
        {
            var gameCore = new GameCore();
            //gameCore.OnIntroEnd += OnIntroEnd;
            DataContext = gameCore;
        }

        private void OnIntroEnd(Intro i, EventArgs e)
        {
            StartHome();
        }

        private void OnBackHome(GameCreation i, EventArgs e)
        {
            StartHome();
        }

        private void OnNewGame(Home i, EventArgs e)
        {
            StartNewGame();
        }

        private void OnLoadGame(Home i, EventArgs e)
        {
            StartLoadGame();
        }

        private void OnStartGame(GameCreation i, GameEventArgs e)
        {
            StartGame(e.Game);
        }
    }
}
