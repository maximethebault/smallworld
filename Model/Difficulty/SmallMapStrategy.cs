namespace Model.Difficulty
{
    class SmallMapStrategy : IDifficultyStrategy
    {
        public int GetNbUnitsPerRace()
        {
            return 6;
        }

        public int GetNbTurns()
        {
            return 20;
        }

        public int GetMapWidth()
        {
            return 10;
        }

        public int GetNbTiles()
        {
            return GetMapWidth() * GetMapWidth();
        }

        public int GetNbTileTypes()
        {
            return 4;
        }

        public int GetMinPlayer()
        {
            return 2;
        }

        public int GetMaxPlayer()
        {
            return 2;
        }

        public bool IsMaxTurnNumberReached(int numberTurnPlayed)
        {
            return numberTurnPlayed >= GetNbTurns();
        }
    }
}
