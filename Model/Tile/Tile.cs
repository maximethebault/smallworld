using System;
using System.Drawing;
using System.Xml.Serialization;

namespace Model.Tile
{
    [Serializable]
    abstract public class Tile : ITile
    {
        [XmlIgnore]
        private Bitmap Bitmap
        {
            get;
            set;
        }

        public void SetBitmap(Bitmap bitmap)
        {
            Bitmap = bitmap;
        }

        public Bitmap GetBitmap()
        {
            return Bitmap;
        }

        public virtual bool IsPlain()
        {
            return false;
        }

        public virtual bool IsDesert()
        {
            return false;
        }

        public virtual bool IsMountain()
        {
            return false;
        }

        public virtual bool IsForest()
        {
            return false;
        }
    }
}
