using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Model.Game;
using Model.Map;
using UI.Screen.Game.Core.Map.ViewModel;

namespace UI.Screen.Game.Core.Map
{
    /// <summary>
    /// Logique d'interaction pour Map.xaml
    /// </summary>
    public partial class Map : UserControl
    {
        public ObservableCollection<Tile> Tiles { get; set; }

        public BitmapImage[] TilesTexture { get; set; }

        public IGame Game { get; set; }

        public Map()
        {
            Tiles = new ObservableCollection<Tile>();
            InitializeComponent();
        }

        public void StartLoading()
        {
            var difficulty = Game.DifficultyStrategy;
            for (var i = 0; i < difficulty.GetMapWidth(); ++i) // i -> y
            {
                for (var j = 0; j < difficulty.GetMapWidth(); ++j) // j -> x
                {
                    var tileObject = Game.Map.TileAtPosition(new HexaPosition(i, j));
                    var tileTextureIndex = 0;
                    if (tileObject.IsDesert())
                    {
                        tileTextureIndex = 0;
                    }
                    else if (tileObject.IsForest())
                    {
                        tileTextureIndex = 1;
                    }
                    else if (tileObject.IsMountain())
                    {
                        tileTextureIndex = 2;
                    }
                    else if (tileObject.IsPlain())
                    {
                        tileTextureIndex = 3;
                    }
                    var tile = new Tile { Column = i, Row = j, Texture = TilesTexture[tileTextureIndex]};
                    Tiles.Add(tile);
                }
            }
        }
    }
}
