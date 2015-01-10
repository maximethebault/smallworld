using System;
using System.Windows.Media.Imaging;
using Model.Game;
using Model.Map;
using Model.Unit.Exception;
using UI.Screen.Game.Core.Map.ViewModel;
using UI.Screen.Game.Core.Player.ViewModel;
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
        public PlayersViewModel PlayersViewModel { get; set; }

        private bool _moveErrorVisibility;
        public bool MoveErrorVisibility
        {
            get { return _moveErrorVisibility; }
            set
            {
                _moveErrorVisibility = value;
                RaisePropertyChanged("MoveErrorVisibility");
            }
        }

        public GameCoreViewModel(IGame game, BitmapImage[] tilesTexture, BitmapImage[] racesTexture)
        {
            Game = game;
            TilesTexture = TilesTexture;
            RacesTexture = RacesTexture;

            MapViewModel = new MapViewModel(game, tilesTexture, racesTexture);
            UnitsViewModel = new UnitsViewModel(game);
            PlayersViewModel = new PlayersViewModel(game);

            MapViewModel.OnSelectTile += SelectTile;
            MapViewModel.OnMoveToTile += MoveToTile;

            PlayersViewModel.OnCurrentPlayerChange += CurrentPlayerChange;
        }

        private void SelectTile(TileViewModel t, EventArgs e)
        {
            UnitsViewModel.Units = t.Units;
        }

        private void MoveToTile(TileViewModel tile, EventArgs e)
        {
            var unit = UnitsViewModel.SelectedUnit;
            if (unit == null)
            {
                // no unit is selected, cancel the action
                return;
            }
            try
            {
                Game.MoveUnit(unit.Model, PositionFactory.GetHexaPosition(tile.Column, tile.Row));
            }
            catch (UnitMovementUnauthorized ex)
            {
                MoveErrorVisibility = true;
            }
            if (Game.Fight != null)
            {
                //TODO
                return;
            }
            MapViewModel.Refresh();
        }

        private void CurrentPlayerChange(PlayersViewModel t, EventArgs e)
        {
            PlayersViewModel.Refresh();
            MapViewModel.Refresh();
            UnitsViewModel.Refresh();
        }
    }
}
