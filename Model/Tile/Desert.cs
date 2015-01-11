using System;

namespace Model.Tile
{
    [Serializable()]
    public class Desert : Tile
    {
        public override bool IsDesert()
        {
            return true;
        }
    }
}