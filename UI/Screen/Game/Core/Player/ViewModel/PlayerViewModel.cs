using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Game;
using Model.Player;
using Model.Unit;

namespace UI.Screen.Game.Core.Player.ViewModel
{
    class PlayerViewModel : ViewModelBase
    {
        private bool _isCurrentPlayer;
        public bool IsCurrentPlayer
        {
            get { return _isCurrentPlayer; }
            set
            {
                _isCurrentPlayer = value;
                RaisePropertyChanged("IsCurrentPlayer");
            }
        }

        public IPlayer Model { get; set; }

        public IGame Game { get; set; }

        public int Row { get; set; }

        public PlayerViewModel(IGame game, IPlayer model, int i)
        {
            Game = game;
            Model = model;
            Row = i;

            Refresh();
        }

        public void Refresh()
        {
            if (Game.CurrentPlayer != null)
            {
                IsCurrentPlayer = Game.CurrentPlayer.Equals(Model);
            }
        }
    }
}
