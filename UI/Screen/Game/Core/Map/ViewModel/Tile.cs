using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace UI.Screen.Game.Core.Map.ViewModel
{
    public class Tile : ViewModelBase
    {
        public int Column { get; set; }
        public int Row { get; set; }
        public BitmapImage Texture { get; set; }
    }
}
