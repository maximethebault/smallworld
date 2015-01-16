using System;
using System.Threading.Tasks;
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
using UI.Screen.Game.Core.Win;

namespace UI.Screen.Game.Core
{
    class GameCoreViewModel : ViewModelBase
    {
        public BitmapImage[] TilesTexture { get; set; }
        public BitmapImage[] RacesTexture { get; set; }

        public IGame Model { get; set; }

        public MapViewModel MapViewModel { get; set; }
        public UnitsViewModel UnitsViewModel { get; set; }
        public PlayersViewModel PlayersViewModel { get; set; }
        public FightViewModel FightViewModel { get; set; }
        public MenuViewModel MenuViewModel { get; set; }
        public WinViewModel WinViewModel { get; set; }

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
        public ICommand HideMoveErrorCommand { get; private set; }

        public event GameExitEventHandler OnGameExit;
        public delegate void GameExitEventHandler(GameCoreViewModel t, EventArgs e);

        public GameCoreViewModel(IGame game, BitmapImage[] tilesTexture, BitmapImage[] racesTexture)
        {
            Model = game;
            TilesTexture = TilesTexture;
            RacesTexture = RacesTexture;

            MenuCommand = new DelegateCommand(o => ToggleMenu());
            HideMoveErrorCommand = new DelegateCommand(o => HideMoveError());

            MapViewModel = new MapViewModel(game, tilesTexture, racesTexture);
            UnitsViewModel = new UnitsViewModel(game);
            PlayersViewModel = new PlayersViewModel(game);
            FightViewModel = new FightViewModel(game, racesTexture);
            MenuViewModel = new MenuViewModel(game);
            WinViewModel = new WinViewModel(game);

            MapViewModel.OnSelectTile += SelectTile;
            MapViewModel.OnMoveToTile += MoveToTile;

            UnitsViewModel.OnSelectUnit += SelectUnit;

            PlayersViewModel.OnCurrentPlayerChange += CurrentPlayerChange;

            FightViewModel.OnFightEnd += FightEnd;

            MenuViewModel.OnGameExit += GameExit;

            WinViewModel.OnGameExit += GameExit;
        }

        private void ToggleMenu()
        {
            // TODO: check for game end overlay presence first
            MenuViewModel.Visible = !MenuViewModel.Visible;
        }

        private void HideMoveError()
        {
            MoveErrorVisibility = false;
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
                Model.MoveUnit(unit.Model, PositionFactory.GetHexaPosition(tile.Column, tile.Row));
            }
            catch (UnitMovementUnauthorized ex)
            {
                MoveErrorVisibility = true;
            }
            FightViewModel.Refresh();
            MapViewModel.Refresh();
        }

        private void SelectUnit(UnitsViewModel t, EventArgs e)
        {
            if (t.SelectedUnit == null)
            {
                return;
            }

            Task.Factory.StartNew(() =>
            {
                // we take advantage of the fact that MVVM mechanisms automatically handle the UI thread update problematic
                // getting advice can be a long operation, it needs to be asynchronous
                var advices = Model.ComputeAdvices(UnitsViewModel.SelectedUnit.Model);
                MapViewModel.RefreshAdvices(advices);
            });

        }

        private void CurrentPlayerChange(PlayersViewModel t, EventArgs e)
        {
            PlayersViewModel.Refresh();
            MapViewModel.Refresh();
            UnitsViewModel.Refresh();
        }

        private void FightEnd(FightViewModel t, EventArgs e)
        {
            PlayersViewModel.Refresh();
            MapViewModel.Refresh();
            UnitsViewModel.Refresh();
        }

        private void GameExit(ViewModelBase t, EventArgs e)
        {
            //TODO: dispose of event handler in children view model

            // event unsubscription to avoid memory leak
            MapViewModel.OnSelectTile -= SelectTile;
            MapViewModel.OnMoveToTile -= MoveToTile;

            PlayersViewModel.OnCurrentPlayerChange -= CurrentPlayerChange;

            FightViewModel.OnFightEnd -= FightEnd;

            MenuViewModel.OnGameExit -= GameExit;

            WinViewModel.OnGameExit -= GameExit;

            //emit the game exit event
            if (OnGameExit != null)
            {
                OnGameExit(this, null);
            }
        }
    }
}
