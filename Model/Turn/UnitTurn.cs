using System;
using System.Collections.Generic;
using Model.Unit;

namespace Model.Turn
{
    public class UnitTurn : IDUnitTurn
    {

        public IUnit CurrentUnit
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
    }
}