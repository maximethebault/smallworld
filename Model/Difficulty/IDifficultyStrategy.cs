namespace Model.Difficulty
{
    public interface IDifficultyStrategy
    {

        int GetNbUnitsPerRace();

        int GetNbTurns();

        int GetMapWidth();

        int GetNbTiles();

        int GetNbTileTypes();

        int GetMinPlayer();

        int GetMaxPlayer();

        bool IsMaxTurnNumberReached(int numberTurnPlayed);
    }
}
