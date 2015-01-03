﻿using System.Collections.Generic;
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
        int ComputeScore();
        bool HasUnitLeft();
        List<IDUnit> IDUnitsAt(IPosition position);
    }
}