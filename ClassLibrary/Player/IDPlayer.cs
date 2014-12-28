using System.Collections.Generic;
using ClassLibrary.Map;
using ClassLibrary.Race;
using ClassLibrary.Unit;

namespace ClassLibrary.Player
{
    public interface IDPlayer : IPlayer
    {
        List<IDUnit> IDUnits { get; set; }
        IDRace IDRace { get; }
        void RemoveUnit(IUnit unit);
        int ComputeScore();
        bool HasUnitLeft();
        List<IDUnit> IDUnitsAt(IPosition position);
    }
}