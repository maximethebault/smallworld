namespace Model.Fight.Exception
{
    public class NoFightException : System.Exception
    {
        public NoFightException(string message)
            : base(message)
        {
        }
    }
}