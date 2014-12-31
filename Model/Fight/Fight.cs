using System;
using System.Diagnostics;
using Model.Fight.Strategy;
using Model.Unit;

namespace Model.Fight
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

        public IFightStrategy FightStrategy { get; set; }

        private bool _finished { get; set; }

        public Fight(IDUnit attacker, IDUnit defender, IFightStrategy fightStrategy)
        {
            IDAttacker = attacker;
            IDDefender = defender;
            Winner = null;
            Loser = null;
            ElapsedRounds = 0;
            TotalRounds = 0;
            FightStrategy = fightStrategy;
            FightStrategy.IDAttacker = attacker;
            FightStrategy.IDDefender = defender;
        }

        private void StartFight()
        {
            TotalRounds = FightStrategy.ComputeTotalRounds();
        }

        public void NextRound()
        {
            if (TotalRounds == 0)
            {
                StartFight();
            }
            ElapsedRounds++;
            var attackerWinRatio = FightStrategy.ComputeAttackerWinRatio();
            var r = new Random();
            if (r.NextDouble() <= attackerWinRatio)
            {
                // attacker wins
                IDDefender.DecrementLifePoint();
                if (IDDefender.IsDead())
                {
                    Winner = IDAttacker;
                    Loser = IDDefender;
                    IDAttacker.Kill(IDDefender);
                    FinishFight();
                    return;
                }
            }
            else
            {
                // defender wins
                IDAttacker.DecrementLifePoint();
                if (IDAttacker.IsDead())
                {
                    Winner = IDDefender;
                    Loser = IDAttacker;
                    IDDefender.Kill(IDAttacker);
                    FinishFight();
                    return;
                }
            }
            if (ElapsedRounds == TotalRounds)
            {
                FinishFight();
            }
        }

        private void FinishFight()
        {
            _finished = true;
        }

        public bool IsFinished()
        {
            return _finished;
        }
    }
}