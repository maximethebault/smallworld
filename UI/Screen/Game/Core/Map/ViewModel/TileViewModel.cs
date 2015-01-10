using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Media.Imaging;
using UI.Screen.Game.Core.Unit.ViewModel;

namespace UI.Screen.Game.Core.Map.ViewModel
{
    public class TileViewModel : ViewModelBase
    {
        public int Column { get; set; }
        public int Row { get; set; }
        public BitmapImage Texture { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }

        public bool HasUnit
        {
            get { return Units.Count > 0; }
        }

        private ObservableCollection<UnitViewModel> _units;
        public ObservableCollection<UnitViewModel> Units
        {
            get { return _units; }
            set
            {
                _units = value;
                RaisePropertyChanged("Units");
                RaisePropertyChanged("HasUnit");
            }
        }

        private BitmapImage _unitTexture;

        public BitmapImage UnitTexture
        {
            get { return _unitTexture; }
            set
            {
                _unitTexture = value;
                RaisePropertyChanged("UnitTexture");
            }
        }

        public TileViewModel()
        {
            Units = new ObservableCollection<UnitViewModel>();
            Units.CollectionChanged += UnitsCollectionChanged;
        }

        private void UnitsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged("HasUnit");
            RaisePropertyChanged("Units.Count");
        }

        public void Refresh()
        {
            foreach (var unit in Units)
            {
                unit.Refresh();
            }
        }
    }
}
