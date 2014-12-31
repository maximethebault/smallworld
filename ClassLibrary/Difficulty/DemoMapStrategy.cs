namespace ClassLibrary.Difficulty
{
    public class DemoMapStrategy : IDifficultyStrategy
    {

        public int GetNbUnitsPerRace()
        {
            return 4;
        }

        public int GetNbTurns()
        {
            return 5;
        }

        public int GetMapWidth()
        {
            return 6;
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