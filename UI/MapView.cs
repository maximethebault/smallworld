using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Model.Difficulty;
using Model.Game.Builder;
using Model.Map;
using Model.Race;

namespace UI
{
    internal class MapView : Panel
    {
        //méthode pour aficher les tiles
        protected override void OnRender(DrawingContext dc)
        {
            //condition pour éviter d'avoir des erreurs à cause du bin/debug
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            /*Rect myRect = new Rect(0, 0, 700, 700);
            BitmapImage imag = new BitmapImage(new Uri("textures\\homepage.jpg", UriKind.Relative));
            dc.DrawImage(imag, myRect);*/

            var difficulty = new SmallMapStrategy();
            INewGameBuilder newGameBuilder = new NewGameBuilder();
            newGameBuilder.AddPlayer("Kikou", new RaceDwarf());
            newGameBuilder.AddPlayer("Mama", new RaceElf());
            newGameBuilder.SetDifficulty(difficulty);
            var gameCreator = new GameCreator(newGameBuilder);
            var game = gameCreator.CreateGame().GetGame();
            var map = game.Map;

            //Liste des tiles possibles
            var tilesBitmap = new List<BitmapImage>();
            tilesBitmap.Add(new BitmapImage(new Uri("textures\\plaine.png", UriKind.Relative)));
            tilesBitmap.Add(new BitmapImage(new Uri("textures\\foret.png", UriKind.Relative)));
            tilesBitmap.Add(new BitmapImage(new Uri("textures\\desert.png", UriKind.Relative)));
            tilesBitmap.Add(new BitmapImage(new Uri("textures\\ocean.png", UriKind.Relative)));

            //Taille des tiles
            var tileWidth = tilesBitmap[0].Width;
            var tileHeight = tilesBitmap[0].Height;

            //Décalage pour les lignes impaires
            var decalageVertical = (tileHeight - ((Math.Sqrt(Math.Pow(tileHeight / 2, 2) - Math.Pow(tileWidth / 2, 2))) * 2)) / 2;
            var decalageHorizontal = tileWidth / 2;

            double posX, posY;
            for (var i = 0; i < difficulty.GetMapWidth(); ++i) // i -> y
            {
                for (var j = 0; j < difficulty.GetMapWidth(); ++j) // j -> x
                {
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

                    var tile = map.TileAtPosition(new Position(i, j));

                    if (tile.IsDesert())
                    {
                        dc.DrawImage(tilesBitmap[2], new Rect(posX, posY, tileWidth, tileHeight));
                    }
                    else if (tile.IsForest())
                    {
                        dc.DrawImage(tilesBitmap[1], new Rect(posX, posY, tileWidth, tileHeight));
                    }
                    else if (tile.IsMountain())
                    {
                        //Ocean ?
                        dc.DrawImage(tilesBitmap[3], new Rect(posX, posY, tileWidth, tileHeight));
                    }
                    else if (tile.IsPlain())
                    {
                        dc.DrawImage(tilesBitmap[0], new Rect(posX, posY, tileWidth, tileHeight));
                    }
                }
            }
        }
    }
}
