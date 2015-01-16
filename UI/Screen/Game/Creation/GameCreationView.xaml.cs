using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using UI.Screen.Game.Creation.ViewModel;

namespace UI.Screen.Game.Creation
{
    /// <summary>
    /// Logique d'interaction pour GameCreationView.xaml
    /// </summary>
    public partial class GameCreationView : UserControl
    {
        public GameCreationView()
        {
            InitializeComponent();
        }

        private void ButtonMouseEnter(object sender, MouseEventArgs e)
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
    }
}
