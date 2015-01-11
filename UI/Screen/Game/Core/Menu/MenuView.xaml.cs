using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace UI.Screen.Game.Core.Menu
{
    /// <summary>
    /// Logique d'interaction pour FightView.xaml
    /// </summary>
    public partial class MenuView : UserControl
    {
        public MenuView()
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
