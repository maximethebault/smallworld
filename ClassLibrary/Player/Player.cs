using System;
using System.Collections.Generic;
using System.Linq;
using ClassLibrary.Map;
using ClassLibrary.Race;
using ClassLibrary.Unit;

namespace ClassLibrary.Player
{
    public class Player : IDPlayer
    {
        public Player(string name, Race.Race raceImpl)
        {
            Name = name;
            RaceImpl = raceImpl;
            IDUnits = new List<IDUnit>();
        }

        public String Name
        {
            get;
            set;
        }

        public string GetName()
        {
            return Name;
        }

        public Race.Race RaceImpl
        {
            get;
            set;
        }

        public IRace Race
        {
            get
            {
                return RaceImpl;
            }
        }

        public List<IDUnit> IDUnits
        {
            get;
            set;
        }

        public void RemoveUnit(IUnit unit)
        {
            IDUnits.RemoveAll(itUnit => itUnit.Equals(unit));
        }

        public int ComputeScore()
        {
            return IDUnits.Sum(unit => unit.GetScore());
        }

        public bool HasUnitLeft()
        {
            return IDUnits.Count > 0;
        }

        public List<IDUnit> IDUnitsAt(IPosition position)
        {
            return IDUnits.FindAll(unit => unit.Position.Equals(position));
        }

        public List<IUnit> UnitsAt(IPosition position)
        {
            return IDUnitsAt(position).Cast<IUnit>().ToList();
        }
    }
}