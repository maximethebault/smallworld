using System;

namespace ClassLibrary.Tile
{
    public class TileFlyweightFactory
    {
        private Plain _plainTile;
        private Desert _desertTile;
        private Mountain _mountainTile;
        private Forest _forestTile;

        public Tile CreateTile()
        {
            throw new NotImplementedException();
        }
    }
}
