namespace ClassLibrary.Game.Exception
{
    public class TooFewPlayersException : System.Exception
    {
        public TooFewPlayersException(string message) : base(message)
        {
        }
    }
}