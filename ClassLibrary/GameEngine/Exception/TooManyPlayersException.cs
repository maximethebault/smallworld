namespace ClassLibrary.GameEngine.Exception
{
    public class TooManyPlayersException : System.Exception
    {
        public TooManyPlayersException(string message) : base(message)
        {
        }
    }
}