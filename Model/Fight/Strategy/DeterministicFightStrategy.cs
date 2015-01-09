using Model.Unit;

namespace Model.Fight.Strategy
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
