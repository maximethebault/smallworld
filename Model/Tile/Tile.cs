using System;
using System.Drawing;
using System.Xml.Serialization;

namespace Model.Tile
{
    [Serializable]
    abstract public class Tile : ITile
    {
        public abstract int ID { get; }

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
