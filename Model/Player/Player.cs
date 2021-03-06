﻿using System;
using System.Collections.Generic;
using System.Linq;
using Model.Map;
using Model.Race;
using Model.Unit;
using Model.Utils;

namespace Model.Player
{
    [Serializable()]
    public class Player : PropertyChangedNotifier, IDPlayer
    {
        #region Properties
        public String Name { get; set; }

        private int _score;
        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                RaisePropertyChanged("Score");
            }
        }

        public IDRace IDRace { get; set; }

        public IRace Race
        {
            get { return IDRace; }
        }

        public List<IDUnit> IDUnits { get; set; }
        #endregion Properties

        public Player(string name, IDRace race)
        {
            Name = name;
            IDRace = race;
            IDUnits = new List<IDUnit>();
            Score = 0;
        }

        public void RemoveUnit(IUnit unit)
        {
            IDUnits.RemoveAll(itUnit => itUnit.Equals(unit));
        }

        public void ComputeScore()
        {
            var score = 0;
            IEnumerable<IDUnit> alreadyProcessed = new List<IDUnit>();
            foreach (var unit in IDUnits)
            {
                if (alreadyProcessed.Contains(unit))
                {
                    continue;
                }
                var position = unit.Position;
                var unitsOnSameTile = IDUnits.Where(sameTileUnit => sameTileUnit.Position.Equals(position));
                var tileScore = 0;
                tileScore += unitsOnSameTile.Sum(sameTileUnit => sameTileUnit.ComputeScore());
                tileScore -= unitsOnSameTile.Count() - 1;
                score += tileScore >= 0 ? tileScore : 0;
                alreadyProcessed = alreadyProcessed.Union(unitsOnSameTile);
            }
            Score = score;
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