using System;

namespace Model.Tile
{
    [Serializable()]
    public class Desert : Tile
    {
        public override int ID
        {
            get { return 0; }
        }

        public override bool IsDesert()
        {
            return true;
        }
    }
}