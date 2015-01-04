using System;
using System.Collections.Generic;

namespace UI.Screen.Game.Creation.ViewModel
{
    public class Player : ViewModelBase
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

        public Race[] Races { get; set; }

        public Player(int index, IReadOnlyList<String> races)
        {
            Name = "";
            Index = index;
            SelectedRace = -1;
            PlaceHolder = "Joueur " + (index + 1);
            Races = new Race[races.Count];
            for (var i = 0; i < races.Count; i++)
            {
                Races[i] = new Race(races[i], i);
            }
        }
    }
}