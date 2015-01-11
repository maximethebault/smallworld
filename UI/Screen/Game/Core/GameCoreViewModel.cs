using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Model.Game;
using Model.Map;
using Model.Unit.Exception;
using UI.Screen.Game.Core.Fight.ViewModel;
using UI.Screen.Game.Core.Map.ViewModel;
using UI.Screen.Game.Core.Menu.ViewModel;
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
        public FightViewModel FightViewModel { get; set; }
        public MenuViewModel MenuViewModel { get; set; }

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

        public ICommand MenuCommand { get; private set; }

        public event GameExitEventHandler OnGameExit;
        public delegate void GameExitEventHandler(GameCoreViewModel t, EventArgs e);

        public GameCoreViewModel(IGame game, BitmapImage[] tilesTexture, BitmapImage[] racesTexture)
        {
            Game = game;
            TilesTexture = TilesTexture;
            RacesTexture = RacesTexture;

            MenuCommand = new DelegateCommand(o => ToggleMenu());

            MapViewModel = new MapViewModel(game, tilesTexture, racesTexture);
            UnitsViewModel = new UnitsViewModel(game);
            PlayersViewModel = new PlayersViewModel(game);
            FightViewModel = new FightViewModel(game);
            MenuViewModel = new MenuViewModel(game);

            MapViewModel.OnSelectTile += SelectTile;
            MapViewModel.OnMoveToTile += MoveToTile;

            PlayersViewModel.OnCurrentPlayerChange += CurrentPlayerChange;

            MenuViewModel.OnGameExit += GameExit;
        }

        private void ToggleMenu()
        {
            // TODO: check for game end overlay presence first
            MenuViewModel.Visible = !MenuViewModel.Visible;
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

        private void GameExit(MenuViewModel t, EventArgs e)
        {
            //TODO: dispose of event handler in children view model

            // event unsubscription to avoid memory leak
            MapViewModel.OnSelectTile -= SelectTile;
            MapViewModel.OnMoveToTile -= MoveToTile;

            PlayersViewModel.OnCurrentPlayerChange -= CurrentPlayerChange;

            MenuViewModel.OnGameExit -= GameExit;

            //emit the game exit event
            if (OnGameExit != null)
            {
                OnGameExit(this, null);
            }
        }
    }
}
