using System;

namespace Model.Tile
{
    [Serializable()]
    public class Mountain : Tile
    {
        public override bool IsMountain()
        {
            return true;
        }
    }
}