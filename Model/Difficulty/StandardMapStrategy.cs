namespace Model.Difficulty
{
    public class StandardMapStrategy : IDifficultyStrategy
    {
        public int GetNbUnitsPerRace()
        {
            return 8;
        }

        public int GetNbTurns()
        {
            return 30;
        }

        public int GetMapWidth()
        {
            return 14;
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