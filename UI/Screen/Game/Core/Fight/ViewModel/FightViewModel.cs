using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Practices.Prism.Commands;
using Model.Fight;
using Model.Game;
using Model.Unit;

namespace UI.Screen.Game.Core.Fight.ViewModel
{
    class FightViewModel : ViewModelBase
    {
        private IGame Game { get; set; }

        private bool _inProgress;

        public bool InProgress
        {
            get { return _inProgress; }
            private set
            {
                _inProgress = value;
                RaisePropertyChanged("InProgress");
            }
        }

        private IFight _model;

        public IFight Model
        {
            get { return _model; }
            set
            {
                _model = value;
                RaisePropertyChanged("Model");
            }
        }

        private UnitViewModel _attacker;

        public UnitViewModel Attacker
        {
            get { return _attacker; }
            set
            {
                _attacker = value;
                RaisePropertyChanged("Attacker");
            }
        }

        private UnitViewModel _defender;

        public UnitViewModel Defender
        {
            get { return _defender; }
            set
            {
                _defender = value;
                RaisePropertyChanged("Defender");
            }
        }

        private BitmapImage[] RacesTexture { get; set; }

        private bool _fightInitVisible;

        public bool FightInitVisible
        {
            get { return _fightInitVisible; }
            set
            {
                _fightInitVisible = value;
                RaisePropertyChanged("FightInitVisible");
            }
        }

        private bool _fightRoundVisible;

        public bool FightRoundVisible
        {
            get { return _fightRoundVisible; }
            set
            {
                _fightRoundVisible = value;
                RaisePropertyChanged("FightRoundVisible");
            }
        }

        private bool _fightWinnerVisible;

        public bool FightWinnerVisible
        {
            get { return _fightWinnerVisible; }
            set
            {
                _fightWinnerVisible = value;
                RaisePropertyChanged("FightWinnerVisible");
            }
        }

        private IUnit _latestWinner;
        public IUnit LatestWinner
        {
            get { return _latestWinner; }
            set
            {
                _latestWinner = value;
                RaisePropertyChanged("LatestWinner");
            }
        }

        public ICommand PropelFightCommand { get; private set; }

        public event FightEndEventHandler OnFightEnd;
        public delegate void FightEndEventHandler(FightViewModel t, EventArgs e);

        public FightViewModel(IGame game, BitmapImage[] racesTexture)
        {
            Game = game;
            InProgress = false;
            RacesTexture = racesTexture;
            FightInitVisible = true;
            FightRoundVisible = false;
            FightWinnerVisible = false;

            PropelFightCommand = new DelegateCommand(o => PropelFight());

            Refresh();
        }

        private void PropelFight()
        {
            if (Model.Finished)
            {
                if (OnFightEnd != null)
                {
                    OnFightEnd(this, null);
                }
                Reset();
                return;
            }
            LatestWinner = Game.NextFightRound();
            FightInitVisible = false;
            if (Model.Finished)
            {
                FightRoundVisible = false;
                FightWinnerVisible = true;
            }
            else
            {
                FightRoundVisible = true;
            }
        }

        public void Refresh()
        {
            if (Game.Fight == null)
            {
                Reset();
                return;
            }
            if (Game.Fight != Model)
            {
                Model = Game.Fight;
                var raceAttacker = Utils.Unit.ConvertRaceNameToID(Model.Attacker.Player.Race.Name);
                Attacker = new UnitViewModel(Model.Attacker, RacesTexture[raceAttacker]);
                var raceDefender = Utils.Unit.ConvertRaceNameToID(Model.Defender.Player.Race.Name);
                Defender = new UnitViewModel(Model.Defender, RacesTexture[raceDefender]);
            }
            InProgress = true;
        }

        private void Reset()
        {
            InProgress = false;
            FightInitVisible = true;
            FightRoundVisible = false;
            FightWinnerVisible = false;
        }
    }
}
