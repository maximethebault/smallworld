namespace Model.Map
{
    public class PositionFactory
    {
        public static IPosition GetHexaPosition(int x, int y)
        {
            return new HexaPosition(x, y);
        }
    }
}
