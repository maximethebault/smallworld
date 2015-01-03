using System;
using System.Windows;
using System.Windows.Controls;

namespace UI.Screen.Home
{
    /// <summary>
    /// Logique d'interaction pour Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public event NewGameEventHandler OnNewGame;
        public delegate void NewGameEventHandler(Home i, EventArgs e);

        public event LoadGameEventHandler OnLoadGame;
        public delegate void LoadGameEventHandler(Home i, EventArgs e);

        public Home()
        {
            InitializeComponent();
        }

        private void New_Game(object sender, RoutedEventArgs e)
        {
            if (OnNewGame != null)
            {
                OnNewGame(this, null);
            }
        }

        private void Load_Game(object sender, RoutedEventArgs e)
        {
            if (OnLoadGame != null)
            {
                OnLoadGame(this, null);
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
