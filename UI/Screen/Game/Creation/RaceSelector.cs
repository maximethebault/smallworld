using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UI.Screen.Game.Creation
{
    public class RaceSelector : ViewModelBase
    {

        private bool _isEnabled;
        public PathItem Race { get; set; }

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

        public RaceSelector(PathItem race, int i)
        {
            Race = race;
            Index = i;
            IsSelected = false;
            IsEnabled = true;
        }
    }
}
