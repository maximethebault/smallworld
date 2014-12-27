using System;
using System.Collections.Generic;
using ClassLibrary.Unit;

namespace ClassLibrary.Turn
{
    public class UnitTurn : IUnitTurn
    {

        public Unit.Unit CurrentUnit
        {
            get;
            set;
        }

        private IEnumerable<Unit.Unit> playedUnits;

        public UnitTurn()
        {
            playedUnits = new List<Unit.Unit>();
        }

        public void GetNextPlayableUnit()
        {
            throw new NotImplementedException();
        }

        public void FinishUnitTurn()
        {
            throw new NotImplementedException();
        }

        public IUnit GetCurrentUnit()
        {
            return CurrentUnit;
        }
    }
}