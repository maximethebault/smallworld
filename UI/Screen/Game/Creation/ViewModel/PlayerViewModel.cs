using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace UI.Screen.Game.Creation.ViewModel
{
    class PlayerViewModel : ViewModelBase
    {
        private string _name;

        public String Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NameErrorVisibility = false;
            }
        }

        public GameCreationViewModel GameCreationViewModel { get; set; }

        private bool _nameErrorVisibility;

        public bool NameErrorVisibility
        {
            get { return _nameErrorVisibility; }
            set
            {
                if (_nameErrorVisibility == value)
                    return;
                _nameErrorVisibility = value;
                RaisePropertyChanged("NameErrorVisibility");
            }
        }
        public int Index { get; set; }

        private int _selectedRace;

        public int SelectedRace
        {
            get { return _selectedRace; }
            set
            {
                GameCreationViewModel.RaceSelected(this, _selectedRace, value);
                _selectedRace = value;
                RaceErrorVisibility = false;
            }
        }

        private bool _raceErrorVisibility;

        public bool RaceErrorVisibility
        {
            get { return _raceErrorVisibility; }
            set
            {
                if (_raceErrorVisibility == value)
                    return;
                _raceErrorVisibility = value;
                RaisePropertyChanged("RaceErrorVisibility");
            }
        }

        public String PlaceHolder { get; private set; }

        public RaceViewModel[] RacesViewModel { get; set; }

        public PlayerViewModel(GameCreationViewModel gameCreationViewModel, int index, RaceDescription[] racesDescription)
        {
            Name = "";
            GameCreationViewModel = gameCreationViewModel;
            Index = index;
            SelectedRace = -1;
            PlaceHolder = "Joueur " + (index + 1);
            RacesViewModel = new RaceViewModel[racesDescription.Length];
            for (var i = 0; i < racesDescription.Length; i++)
            {
                RacesViewModel[i] = new RaceViewModel(racesDescription[i], i);
            }
        }
    }
}