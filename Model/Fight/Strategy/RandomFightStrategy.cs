using System;
using System.Diagnostics;
using Model.Unit;

namespace Model.Fight.Strategy
{
    [Serializable()]
    public class RandomFightStrategy : IFightStrategy
    {
        public IDUnit IDAttacker { get; set; }

        public IDUnit IDDefender { get; set; }

        public RandomFightStrategy()
        {
            IDAttacker = null;
            IDDefender = null;
        }

        public int ComputeTotalRounds()
        {
            if(IDAttacker == null || IDDefender == null)
                throw new System.Exception("RandomFightStrategy requires an attacker & a defender");
            var random = new Random();
            // the maximum for the random method is exclusive, that's why we add 1
            var maxRounds = Math.Max(IDAttacker.HealthPoint, IDDefender.HealthPoint) + 3;
            Debug.Assert(maxRounds >= 4);
            return random.Next(3, maxRounds);
        }

        public float ComputeAttackerWinRatio()
        {
            if (IDAttacker == null || IDDefender == null)
                throw new System.Exception("RandomFightStrategy requires an attacker & a defender");
            float winRatio;
            if (IDAttacker.AttackPoint > IDDefender.DefensePoint)
            {
                winRatio = 0.5f + (1.0f - (IDDefender.DefensePoint / IDAttacker.AttackPoint)) * 0.5f;
            }
            else
            {
                winRatio = 0.5f - (1.0f - (IDAttacker.AttackPoint / IDDefender.DefensePoint)) * 0.5f;
            }
            return winRatio;
        }
    }
}
