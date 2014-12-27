using System.Collections.Generic;

namespace ClassLibrary.Difficulty
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

        // TODO: move somewhere else?
        void PlaceUnitsOnMap(List<Unit.Unit> units, Map.Map map);
    }
}
