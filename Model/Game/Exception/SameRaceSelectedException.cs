namespace Model.Game.Exception
{
    public class SameRaceSelectedException : System.Exception
    {
        public SameRaceSelectedException(string message) : base(message)
        {
        }
    }
}