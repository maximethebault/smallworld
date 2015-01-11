using System;

namespace Model.Tile
{
    [Serializable()]
    public class Plain : Tile
    {
        public override bool IsPlain()
        {
            return true;
        }
    }
}