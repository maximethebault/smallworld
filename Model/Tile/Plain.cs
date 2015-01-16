using System;

namespace Model.Tile
{
    [Serializable()]
    public class Plain : Tile
    {
        public override int ID
        {
            get { return 3; }
        }

        public override bool IsPlain()
        {
            return true;
        }
    }
}