using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private int SelectedIndex
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

        public ICommand EndTurnCommand { get; private set; }

        public event CurrentPlayerChangeEventHandler OnCurrentPlayerChange;
        public delegate void CurrentPlayerChangeEventHandler(PlayersViewModel t, EventArgs e);

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

            EndTurnCommand = new DelegateCommand(o => EndTurn());
        }

        private void EndTurn()
        {
            if (Game.Finished || Game.Fight != null)
            {
                return;
            }
            Game.PropelGame();
            if (OnCurrentPlayerChange != null)
            {
                OnCurrentPlayerChange(this, null);
            }
        }

        public void Refresh()
        {
            foreach (var player in Players)
            {
                player.Refresh();
            }
        }
    }
}
