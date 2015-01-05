using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Model.Game;

namespace UI.Screen.Game.Core.Map
{
    /// <summary>
    /// Logique d'interaction pour Map.xaml
    /// </summary>
    public partial class Map : UserControl
    {
        public static readonly DependencyProperty TilesProperty = DependencyProperty.Register
            (
                 "Tiles",
                 typeof(BitmapImage[]),
                 typeof(Map),
                 new PropertyMetadata(default(ItemCollection))
            );

        public BitmapImage[] Tiles
        {
            get { return (BitmapImage[])GetValue(TilesProperty); }
            set { SetValue(TilesProperty, value); }
        }

        public static readonly DependencyProperty GameProperty = DependencyProperty.Register
            (
                 "Game",
                 typeof(IGame),
                 typeof(Map),
                 new PropertyMetadata(default(ItemCollection))
            );

        public IGame Game
        {
            get { return (IGame)GetValue(GameProperty); }
            set { SetValue(GameProperty, value); }
        }

        public Map()
        {
            InitializeComponent();
        }
    }
}
