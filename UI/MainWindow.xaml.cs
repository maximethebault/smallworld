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
            //newGame.On += OnIntroEnd;
            DataContext = newGame;
        }

        private void StartLoadGame()
        {
            var newGame = new Intro();
            newGame.OnIntroEnd += OnIntroEnd;
            DataContext = newGame;
        }

        public void OnIntroEnd(Intro i, EventArgs e)
        {
            StartHome();
        }

        public void OnNewGame(Home i, EventArgs e)
        {
            StartNewGame();
        }

        public void OnLoadGame(Home i, EventArgs e)
        {
            StartLoadGame();
        }
    }
}
