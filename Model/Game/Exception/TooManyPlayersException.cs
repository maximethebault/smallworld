﻿namespace Model.Game.Exception
{
    public class TooManyPlayersException : System.Exception
    {
        public TooManyPlayersException(string message) : base(message)
        {
        }
    }
}