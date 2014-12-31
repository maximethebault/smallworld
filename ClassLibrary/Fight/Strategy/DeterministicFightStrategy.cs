using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Unit;

namespace ClassLibrary.Fight.Strategy
{
    public class DeterministicFightStrategy : IFightStrategy
    {
        public IDUnit IDAttacker { get; set; }

        public IDUnit IDDefender { get; set; }

        public int TotalRounds { get; set; }

        public int AttackerWinRatio { get; set; }

        public DeterministicFightStrategy(int totalRounds, int attackerWinRatio)
        {
            TotalRounds = totalRounds;
            AttackerWinRatio = attackerWinRatio;
        }

        public int ComputeTotalRounds()
        {
            return TotalRounds;
        }

        public float ComputeAttackerWinRatio()
        {
            return AttackerWinRatio;
        }
    }
}
