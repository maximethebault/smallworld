namespace ClassLibrary.GameEngine.Exception
{
    public class TooFewPlayersException : System.Exception
    {
        public TooFewPlayersException(string message) : base(message)
        {
        }
    }
}