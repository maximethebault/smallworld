using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Model.Unit;

namespace UI.Screen.Game.Core.Fight.ViewModel
{
    class UnitViewModel : ViewModelBase
    {
        public BitmapImage RaceTexture { get; set; }

        public IUnit Model { get; set; }

        public UnitViewModel(IUnit model, BitmapImage raceTexture)
        {
            Model = model;
            RaceTexture = raceTexture;
        }
    }
}
