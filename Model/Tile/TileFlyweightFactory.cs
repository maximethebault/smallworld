namespace Model.Tile
{
    public class TileFlyweightFactory
    {
        static private readonly Plain PlainTile = new Plain();
        static private readonly Desert DesertTile = new Desert();
        static private readonly Mountain MountainTile = new Mountain();
        static private readonly Forest ForestTile = new Forest();

        static public Tile CreateTile(int tileType)
        {
            switch (tileType)
            {
                case 0:
                    return PlainTile;
                case 1:
                    return DesertTile;
                case 2:
                    return MountainTile;
                case 3:
                    return ForestTile;
                default:
                    return PlainTile;
            }
        }
    }
}
