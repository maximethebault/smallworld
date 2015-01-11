using System;

namespace Model.Tile
{
    [Serializable()]
    public class Forest : Tile
    {
        public override bool IsForest()
        {
            return true;
        }
    }
}