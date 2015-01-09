using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Model.Game;

namespace UI.Screen.Game.Core.Unit.ViewModel
{
    class UnitsViewModel : ViewModelBase
    {
        public IGame Game { get; set; }

        private bool _visible;
        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                RaisePropertyChanged("Visible");
            }
        }

        private ObservableCollection<UnitViewModel> _units;
        public ObservableCollection<UnitViewModel> Units
        {
            get { return _units; }
            set
            {
                _units = value;
                if (_units != null && _units.Count > 0)
                {
                    Visible = true;
                }
                else
                {
                    Visible = false;
                }
                RaisePropertyChanged("Units");
                RaisePropertyChanged("Visible");
                UnitListUpdated();
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

        public ICommand TargetUpdatedCommand { get; private set; }

        private UnitViewModel _selectedUnit;
        public UnitViewModel SelectedUnit
        {
            get { return _selectedUnit; }
            set
            {
                _selectedUnit = value;
                RaisePropertyChanged("SelectedUnit");
            }
        }

        public UnitsViewModel(IGame game)
        {
            Game = game;
            Visible = false;
            SelectedIndex = -1;

            TargetUpdatedCommand = new DelegateCommand(o => UnitListUpdated());
        }

        public void UnitListUpdated()
        {
            Visible = Units.Count != 0;
            if (Units.Count > 0 && SelectedIndex == -1 && Units.First().IsPlayable)
            {
                SelectedIndex = 0;
            }
        }
    }
}
