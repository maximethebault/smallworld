using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Model.Difficulty;
using Model.Game;
using Model.Game.Builder;
using Model.Map;
using Model.Race;

namespace UI.Screen.Game.Core
{
    internal class MapView : Panel
    {
        /*public static readonly DependencyProperty TilesProperty = DependencyProperty.Register
            (
                 "Tiles",
                 typeof(BitmapImage[]),
                 typeof(MapView),
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
                 typeof(MapView),
                 new PropertyMetadata(default(ItemCollection))
            );

        public IGame Game
        {
            get { return (IGame)GetValue(GameProperty); }
            set { SetValue(GameProperty, value); }
        }*/

        //méthode pour aficher les tiles
        /*protected override void OnRender(DrawingContext dc)
        {
            //condition pour éviter d'avoir des erreurs à cause du bin/debug
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            var map = Game.Map;
            var difficulty = Game.DifficultyStrategy;

            //Taille des tiles
            var tileWidth = Tiles[0].Width;
            var tileHeight = Tiles[0].Height;

            //Décalage pour les lignes impaires
            var decalageVertical = (tileHeight - ((Math.Sqrt(Math.Pow(tileHeight / 2, 2) - Math.Pow(tileWidth / 2, 2))) * 2)) / 2;
            var decalageHorizontal = tileWidth / 2;

            for (var i = 0; i < difficulty.GetMapWidth(); ++i) // i -> y
            {
                for (var j = 0; j < difficulty.GetMapWidth(); ++j) // j -> x
                {
                    double posX, posY;
                    if (j % 2 == 0)
                    {
                        posX = i * tileWidth;
                        posY = j * tileHeight - (decalageVertical * (j - 1));
                    }
                    else
                    {
                        posX = i * tileWidth + decalageHorizontal;
                        posY = j * tileHeight - (decalageVertical * (j - 1));
                    }
                    posY -= decalageVertical;

                    var tile = map.TileAtPosition(new HexaPosition(i, j));

                    if (tile.IsDesert())
                    {
                        dc.DrawImage(Tiles[2], new Rect(posX, posY, tileWidth, tileHeight));
                    }
                    else if (tile.IsForest())
                    {
                        dc.DrawImage(Tiles[1], new Rect(posX, posY, tileWidth, tileHeight));
                    }
                    else if (tile.IsMountain())
                    {
                        //Ocean ?
                        dc.DrawImage(Tiles[3], new Rect(posX, posY, tileWidth, tileHeight));
                    }
                    else if (tile.IsPlain())
                    {
                        dc.DrawImage(Tiles[0], new Rect(posX, posY, tileWidth, tileHeight));
                    }
                }
            }
        }*/
    }
}
