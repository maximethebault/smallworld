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
using ClassLibrary.Difficulty;
using ClassLibrary.Game.Builder;
using ClassLibrary.Map;
using ClassLibrary.Race;

namespace IHM
{
    class MapView : Panel
    {
        //méthode pour aficher les tiles
        protected override void OnRender(DrawingContext dc)
        {
            //condition pour éviter d'avoir des erreurs à cause du bin/debug
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            Rect myRect = new Rect(0, 0, 700, 700);
            BitmapImage imag = new BitmapImage(new Uri("textures\\homepage.jpg", UriKind.Relative));
            dc.DrawImage(imag, myRect);

            var difficulty = new SmallMapStrategy();
            INewGameBuilder newGameBuilder = new NewGameBuilder();
            newGameBuilder.AddPlayer("Kikou", new RaceDwarf());
            newGameBuilder.AddPlayer("Mama", new RaceElf());
            newGameBuilder.SetDifficulty(difficulty);
            var gameCreator = new GameCreator(newGameBuilder);
            var game = gameCreator.CreateGame().GetGame();
            var map = game.Map;

            for (var i = 0; i < difficulty.GetMapWidth(); ++i)
            {
                for (var j = 0; j < difficulty.GetMapWidth(); ++j)
                {
                    var tile = map.TileAtPosition(new Position(i, j));
                    if (tile.IsDesert())
                    {
                        
                    }
                    else if (tile.IsForest())
                    {
                        
                    }
                    else if (tile.IsMountain())
                    {
                        
                    }
                    else if (tile.IsPlain())
                    {
                        
                    }
                }
            }

    }
}
