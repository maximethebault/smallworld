using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Game;

namespace UI.Screen.Game.Core.Fight.ViewModel
{
    class FightViewModel : ViewModelBase
    {
        public IGame Game { get; private set; }

        private bool _inProgress;
        public bool InProgress
        {
            get { return _inProgress; }
            private set
            {
                _inProgress = value;
                RaisePropertyChanged("InProgress");
            }
        }

        public FightViewModel(IGame game)
        {
            Game = game;
            InProgress = false;
        }
    }
}
