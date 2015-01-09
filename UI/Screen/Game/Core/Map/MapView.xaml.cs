using System.Windows.Controls;
using UI.Screen.Game.Core.Map.ViewModel;

namespace UI.Screen.Game.Core.Map
{
    /// <summary>
    /// Logique d'interaction pour MapView.xaml
    /// </summary>
    public partial class MapView : UserControl
    {
        public MapView()
        {
            InitializeComponent();
        }

        public event MapViewModel.MoveToTileEventHandler OnMoveUnit;
    }
}
