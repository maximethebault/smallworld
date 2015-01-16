using System.Windows.Media.Imaging;

namespace UI.Screen.Game.Creation.ViewModel
{
    class RaceViewModel : ViewModelBase
    {
        private bool _isEnabled;

        public RaceDescription RaceDescription { get; set; }

        public int Index { get; set; }

        public bool IsSelected { get; set; }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (_isEnabled == value)
                    return;
                _isEnabled = value;
                RaisePropertyChanged("IsEnabled");
            }
        }

        public RaceViewModel(RaceDescription raceDescription, int i)
        {
            RaceDescription = raceDescription;
            Index = i;
            IsSelected = false;
            IsEnabled = true;
        }
    }
}
