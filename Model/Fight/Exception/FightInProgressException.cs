﻿namespace Model.Fight.Exception
{
    public class FightInProgressException : System.Exception
    {
        public FightInProgressException(string message)
            : base(message)
        {
        }
    }
}