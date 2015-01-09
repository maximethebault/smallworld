namespace Model.Difficulty
{
    public interface IDifficultyStrategy
    {
        int NbUnitsPerRace { get; }

        int NbTurns { get; }

        int MapWidth { get; }

        int NbTiles { get; }

        int NbTileTypes { get; }

        int MinPlayer { get; }

        int MaxPlayer { get; }

        bool IsMaxTurnNumberReached(int numberTurnPlayed);
    }
}
