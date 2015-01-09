using System.Windows.Media.Imaging;

namespace UI.Screen.Game.Creation.ViewModel
{
    public class RaceViewModel : ViewModelBase
    {
        private bool _isEnabled;

        public BitmapImage Image { get; set; }

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

        public RaceViewModel(BitmapImage image, int i)
        {
            Image = image;
            Index = i;
            IsSelected = false;
            IsEnabled = true;
        }
    }
}
