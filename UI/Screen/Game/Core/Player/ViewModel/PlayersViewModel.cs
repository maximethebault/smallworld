using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Model.Game;
using UI.Screen.Game.Core.Unit.ViewModel;

namespace UI.Screen.Game.Core.Player.ViewModel
{
    class PlayersViewModel : ViewModelBase
    {
        public IGame Game { get; set; }

        private ObservableCollection<PlayerViewModel> _players;
        public ObservableCollection<PlayerViewModel> Players
        {
            get { return _players; }
            set
            {
                _players = value;
                RaisePropertyChanged("Players");
            }
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                RaisePropertyChanged("SelectedIndex");
            }
        }

        private PlayerViewModel _currentPlayer;
        public PlayerViewModel CurrentPlayer
        {
            get { return _currentPlayer; }
            set
            {
                _currentPlayer = value;
                RaisePropertyChanged("CurrentPlayer");
            }
        }

        public PlayersViewModel(IGame game)
        {
            Game = game;
            SelectedIndex = -1;

            Players = new ObservableCollection<PlayerViewModel>();
            var i = 0;
            foreach (var player in Game.Players)
            {
                Players.Add(new PlayerViewModel(Game, player, i++));
            }
        }
    }
}
