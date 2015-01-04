using System;

namespace UI.Screen.Game.Creation.ViewModel
{
    public class Race : ViewModelBase
    {
        private bool _isEnabled;

        public String Path { get; set; }

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

        public Race(String path, int i)
        {
            Path = path;
            Index = i;
            IsSelected = false;
            IsEnabled = true;
        }
    }
}
