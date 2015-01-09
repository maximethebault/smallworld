using System;
using System.Windows.Media.Imaging;
using Model.Game;
using Model.Map;
using UI.Screen.Game.Core.Map.ViewModel;
using UI.Screen.Game.Core.Unit.ViewModel;

namespace UI.Screen.Game.Core
{
    class GameCoreViewModel : ViewModelBase
    {
        public BitmapImage[] TilesTexture { get; set; }
        public BitmapImage[] RacesTexture { get; set; }

        public IGame Game { get; set; }

        public MapViewModel MapViewModel { get; set; }
        public UnitsViewModel UnitsViewModel { get; set; }

        public GameCoreViewModel(IGame game, BitmapImage[] tilesTexture, BitmapImage[] racesTexture)
        {
            Game = game;
            TilesTexture = TilesTexture;
            RacesTexture = RacesTexture;

            MapViewModel = new MapViewModel(game, tilesTexture, racesTexture);
            UnitsViewModel = new UnitsViewModel(game);

            MapViewModel.OnSelectTile += SelectTile;
            MapViewModel.OnMoveToTile += MoveToTile;
        }

        private void SelectTile(TileViewModel t, EventArgs e)
        {
            UnitsViewModel.Units = t.Units;
            t.PropertyChanged += (sender, args) =>
            {
                if (!args.PropertyName.Equals("Units")) return;
                UnitsViewModel.Units = ((TileViewModel)sender).Units;
                UnitsViewModel.UnitListUpdated();
            };
        }

        private void MoveToTile(TileViewModel tile, EventArgs e)
        {
            var unit = UnitsViewModel.SelectedUnit;
            if (unit == null)
            {
                // TODO: Error handling, no unit selected
                return;
            }
            Game.MoveUnit(unit.Model, PositionFactory.GetHexaPosition(tile.Column, tile.Row));
            if (Game.Fight != null)
            {
                //TODO
                return;
            }
            MapViewModel.UpdateUnitOnTile();
        }
    }
}
