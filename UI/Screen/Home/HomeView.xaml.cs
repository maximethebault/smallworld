using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace UI.Screen.Home
{
    /// <summary>
    /// Logique d'interaction pour HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void ButtonMouseEneter(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            var storyBoard = (Storyboard)button.TryFindResource("Storyboard");
            if (storyBoard == null) return;
            storyBoard.Begin();
        }

        private void ButtonMouseLeave(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            var storyBoard = (Storyboard)button.TryFindResource("Storyboard");
            if (storyBoard == null) return;
            storyBoard.Stop();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
