namespace Model.Difficulty
{
    class SmallMapStrategy : IDifficultyStrategy
    {
        public int NbUnitsPerRace
        {
            get { return 6; }
        }

        public int NbTurns
        {
            get { return 20; }
        }

        public int MapWidth
        {
            get { return 10; }
        }

        public int NbTiles
        {
            get { return MapWidth*MapWidth; }
        }

        public int NbTileTypes
        {
            get { return 4; }
        }

        public int MinPlayer
        {
            get { return 2; }
        }

        public int MaxPlayer
        {
            get { return 2; }
        }

        public bool IsMaxTurnNumberReached(int numberTurnPlayed)
        {
            return numberTurnPlayed >= NbTurns;
        }
    }
}
