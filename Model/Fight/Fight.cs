using System;
using Model.Fight.Strategy;
using Model.Unit;

namespace Model.Fight
{
    class Fight : IDFight
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

        public IDUnit IDWinner { get; private set; }
        public IUnit Winner
        {
            get { return IDWinner; }
        }

        public IDUnit IDLoser { get; private set; }

        public IUnit Loser
        {
            get { return IDLoser; }
        }

        public IFightStrategy FightStrategy { get; set; }

        private bool _finished { get; set; }

        public Fight(IDUnit attacker, IDUnit defender, IFightStrategy fightStrategy)
        {
            IDAttacker = attacker;
            IDDefender = defender;
            IDWinner = null;
            IDLoser = null;
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
                    IDWinner = IDAttacker;
                    IDLoser = IDDefender;
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
                    IDWinner = IDDefender;
                    IDLoser = IDAttacker;
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