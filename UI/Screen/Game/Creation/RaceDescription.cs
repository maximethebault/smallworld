using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace UI.Screen.Game.Creation
{
    class RaceDescription
    {
        public BitmapImage Image { get; set; }
        public string Description { get; set; }

        public RaceDescription(BitmapImage image, string description)
        {
            Image = image;
            Description = description;
        }
    }
}
