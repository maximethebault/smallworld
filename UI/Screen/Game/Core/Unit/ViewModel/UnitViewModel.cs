using Model.Game;
using Model.Unit;

namespace UI.Screen.Game.Core.Unit.ViewModel
{
    public class UnitViewModel : ViewModelBase
    {
        public int Row { get; set; }

        private bool _isPlayable;
        public bool IsPlayable
        {
            get { return _isPlayable; }
            set
            {
                _isPlayable = value;
                RaisePropertyChanged("IsPlayable");
            }
        }

        public IUnit Model { get; set; }

        public IGame Game { get; set; }

        public UnitViewModel(IGame game, IUnit model)
        {
            Game = game;
            Model = model;
            Refresh();
        }

        public void Refresh()
        {
            if (Game.CurrentPlayer != null)
            {
                IsPlayable = Game.CurrentPlayer.Equals(Model.Player);
            }
        }
    }
}