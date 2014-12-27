using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace IHM
{
    class MapView : Panel
    {
        //méthode pour aficher les tiles
        protected override void OnRender(DrawingContext dc)
        {
            //condition pour éviter d'avoir des erreurs à cause du bin/debug
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            Rect myRect = new Rect(0, 0, 700, 700);
            BitmapImage imag = new BitmapImage(new Uri("textures\\homepage.jpg", UriKind.Relative));
            dc.DrawImage(imag, myRect);
        }

    }
}
