using System;

namespace Model.Tile
{
    [Serializable()]
    public class Forest : Tile
    {
        public override int ID
        {
            get { return 1; }
        }

        public override bool IsForest()
        {
            return true;
        }
    }
}