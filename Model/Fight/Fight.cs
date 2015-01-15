using System;
using Model.Fight.Strategy;
using Model.Unit;
using Model.Utils;

namespace Model.Fight
{
    [Serializable()]
    class Fight : PropertyChangedNotifier, IDFight
    {
        private int _elapsedRounds;
        public int ElapsedRounds
        {
            get { return _elapsedRounds; }
            private set
            {
                _elapsedRounds = value;
                RaisePropertyChanged("ElapsedRounds");
            }
        }

        public int TotalRounds { get; private set; }

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

        private IDUnit _idWinner;
        public IDUnit IDWinner
        {
            get { return _idWinner; }
            private set
            {
                _idWinner = value;
                RaisePropertyChanged("Winner");
            }
        }
        public IUnit Winner
        {
            get { return IDWinner; }
        }

        private IDUnit _idLoser;
        public IDUnit IDLoser
        {
            get { return _idLoser; }
            private set
            {
                _idLoser = value; 
                RaisePropertyChanged("Loser");
            }
        }
        public IUnit Loser
        {
            get { return IDLoser; }
        }

        public IFightStrategy FightStrategy { get; set; }

        private bool _finished;

        public bool Finished
        {
            get { return _finished; }
            private set
            {
                _finished = value;
                RaisePropertyChanged("Finished");
            }
        }

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
            Finished = false;

            StartFight();
        }

        private void StartFight()
        {
            TotalRounds = FightStrategy.ComputeTotalRounds();
        }

        public IUnit NextRound()
        {
            IUnit winner;
            ElapsedRounds++;
            var attackerWinRatio = FightStrategy.ComputeAttackerWinRatio();
            var r = new Random();
            if (r.NextDouble() <= attackerWinRatio)
            {
                // attacker wins
                winner = Attacker;
                IDDefender.DecrementLifePoint();
                if (IDDefender.IsDead())
                {
                    IDWinner = IDAttacker;
                    IDLoser = IDDefender;
                    FinishFight();
                    return winner;
                }
            }
            else
            {
                // defender wins
                winner = Defender;
                IDAttacker.DecrementLifePoint();
                if (IDAttacker.IsDead())
                {
                    IDWinner = IDDefender;
                    IDLoser = IDAttacker;
                    FinishFight();
                    return winner;
                }
            }
            if (ElapsedRounds == TotalRounds)
            {
                FinishFight();
            }
            return winner;
        }

        private void FinishFight()
        {
            Finished = true;
        }
    }
}