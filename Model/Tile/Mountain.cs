using System;

namespace Model.Tile
{
    [Serializable()]
    public class Mountain : Tile
    {
        public override int ID
        {
            get { return 2; }
        }

        public override bool IsMountain()
        {
            return true;
        }
    }
}