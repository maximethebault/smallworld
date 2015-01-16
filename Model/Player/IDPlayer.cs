using System.Collections.Generic;
using System.Collections.ObjectModel;
using Model.Map;
using Model.Race;
using Model.Unit;

namespace Model.Player
{
    public interface IDPlayer : IPlayer
    {
        List<IDUnit> IDUnits { get; set; }
        IDRace IDRace { get; }
        void RemoveUnit(IUnit unit);
        void ComputeScore();
        bool HasUnitLeft();
        List<IDUnit> IDUnitsAt(IPosition position);
    }
}