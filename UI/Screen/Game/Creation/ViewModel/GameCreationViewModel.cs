using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Model.Difficulty;
using Model.Game;
using Model.Game.Builder;
using UI.Screen.Game.Creation.ViewModel;

namespace UI.Screen.Game.Creation
{
    class GameCreationViewModel : ViewModelBase
    {
        public event BackHomeEventHandler OnBackHome;
        public delegate void BackHomeEventHandler(GameCreationViewModel i, EventArgs e);

        public event NewGameEventHandler OnNewGame;
        public delegate void NewGameEventHandler(GameCreationViewModel i, GameEventArgs e);

        public MapDescription[] MapsDescription { get; set; }

        private int _selectedMap;

        public int SelectedMap
        {
            get { return _selectedMap; }
            set
            {
                _selectedMap = value;
                MapErrorVisibility = false;
            }
        }

        private bool _mapErrorVisibility;

        public bool MapErrorVisibility
        {
            get { return _mapErrorVisibility; }
            set
            {
                if (_mapErrorVisibility == value)
                    return;
                _mapErrorVisibility = value;
                RaisePropertyChanged("MapErrorVisibility");
            }
        }

        private RaceDescription[] RacesDescription { get; set; }

        public List<PlayerViewModel> Players { get; set; }

        public ICommand BackHomeCommand { get; private set; }
        public ICommand StartGameCommand { get; private set; }

        public GameCreationViewModel(MapDescription[] mapsDescription, RaceDescription[] racesDescription, int playerCount)
        {
            MapsDescription = mapsDescription;
            SelectedMap = -1;
            MapErrorVisibility = false;
            RacesDescription = racesDescription;
            Players = new List<PlayerViewModel>();
            for (var i = 0; i < playerCount; i++)
            {
                Players.Add(new PlayerViewModel(this, i, RacesDescription));
            }
            BackHomeCommand = new DelegateCommand(o => BackHome());
            StartGameCommand = new DelegateCommand(o => StartGame());
        }

        private void BackHome()
        {
            if (OnBackHome != null)
            {
                OnBackHome(this, null);
            }
        }

        private void StartGame()
        {
            if (OnNewGame == null)
            {
                return;
            }
            var game = BuildGame();
            if (game != null)
            {
                OnNewGame(this, new GameEventArgs(game));
            }
        }
        public void RaceSelected(PlayerViewModel sourcePlayer, int unselected, int selected)
        {
            foreach (var player in Players.Where(player => !ReferenceEquals(player, sourcePlayer)))
            {
                if (unselected > -1)
                {
                    player.RacesViewModel[unselected].IsEnabled = true;
                }
                if (selected > -1)
                {
                    player.RacesViewModel[selected].IsEnabled = false;
                }
            }
        }

        public IGame BuildGame()
        {
            if (SelectedMap == -1)
            {
                MapErrorVisibility = true;
                return null;
            }
            var builder = BuilderFactory.GetNewGameBuilder();
            builder.Difficulty = DifficultyFactory.GetDifficultyByID(SelectedMap);
            foreach (var player in Players)
            {
                if (String.IsNullOrEmpty(player.Name))
                {
                    player.NameErrorVisibility = true;
                    return null;
                }
                if (player.SelectedRace == -1)
                {
                    player.RaceErrorVisibility = true;
                    return null;
                }
                builder.AddPlayer(player.Name, player.SelectedRace);
            }
            var creator = BuilderFactory.GetGameCreator(builder);
            return creator.CreateGame().GetGame();
        }
    }
}
