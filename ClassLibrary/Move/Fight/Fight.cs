using System;
using System.Collections.Generic;
using System.Diagnostics;
using ClassLibrary.Unit;

namespace ClassLibrary.Move.Fight
{
    public class Fight : IDFight
    {
        public int ElapsedRounds { get; set; }
        public int TotalRounds { get; set; }

        public IDUnit IDAttacker { get; private set; }
        public IUnit Attacker
        {
            get { return IDAttacker; }
        }

        public IDUnit IDDefender { get; private set; }
        public IUnit Defender
        {
            get { return IDDefender; }
        }

        public IUnit Winner { get; set; }

        public IUnit Loser { get; set; }

        public Fight(IDUnit attacker, IDUnit defender)
        {
            IDAttacker = attacker;
            IDDefender = defender;
            Winner = null;
            Loser = null;
            ElapsedRounds = 0;
            TotalRounds = ComputeTotalRounds();
        }

        private int ComputeTotalRounds()
        {
            var random = new Random();
            // the maximum for the random method is exclusive, that's why we add 1
            var maxRounds = Math.Max(IDAttacker.HealthPoint, IDDefender.HealthPoint) + 3;
            Debug.Assert(maxRounds >= 4);
            return random.Next(3, maxRounds);
        }

        public void NextRound()
        {
            throw new System.NotImplementedException();
        }

        public bool IsFinished()
        {
            throw new System.NotImplementedException();
        }
    }
}