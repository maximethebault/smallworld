using System.Windows;
using System.Windows.Controls;

namespace UI.Screen.Game.Core
{
    /// <summary>
    /// Logique d'interaction pour GameCoreView.xaml
    /// </summary>
    public partial class GameCoreView : UserControl
    {
        public GameCoreView()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Focus();
        }
    }
}
