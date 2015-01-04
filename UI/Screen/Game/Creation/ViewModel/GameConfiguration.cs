using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Model.Difficulty;
using Model.Game;
using Model.Game.Builder;

namespace UI.Screen.Game.Creation.ViewModel
{
    public class GameConfiguration : ViewModelBase
    {
        public String[] Maps { get; set; }

        private int _selectedMap;

        public int SelectedMap
        {
            get { return _selectedMap; }
            set
            {
                _selectedMap = value;
                MapErrorVisibility = false;
            }
        }

        private bool _mapErrorVisibility;

        public bool MapErrorVisibility
        {
            get { return _mapErrorVisibility; }
            set
            {
                if (_mapErrorVisibility == value)
                    return;
                _mapErrorVisibility = value;
                RaisePropertyChanged("MapErrorVisibility");
            }
        }

        private String[] Races { get; set; }

        public List<Player> Players { get; set; }

        public GameConfiguration(String[] maps, String[] races, int playerCount)
        {
            Maps = maps;
            SelectedMap = -1;
            MapErrorVisibility = false;
            Races = races;
            Players = new List<Player>();
            for (var i = 0; i < playerCount; i++)
            {
                Players.Add(new Player(i, Races));
            }
        }

        public void OnRaceSelection(object sender, SelectionChangedEventArgs e)
        {
            var listbox = sender as RaceSelector;
            if (listbox == null) return;
            var currentPlayer = listbox.DataContext as Player;
            Race unselected = null;
            if (e.RemovedItems.Count > 0)
            {
                unselected = e.RemovedItems[0] as Race;
            }
            Race selected = null;
            if (e.AddedItems.Count > 0)
            {
                selected = e.AddedItems[0] as Race;
            }
            foreach (var player in Players.Where(player => !ReferenceEquals(player, currentPlayer)))
            {
                if (unselected != null)
                {
                    player.Races[unselected.Index].IsEnabled = true;
                }
                if (selected != null)
                {
                    player.Races[selected.Index].IsEnabled = false;
                }
            }
        }

        public IGame BuildGame()
        {
            if (SelectedMap == -1)
            {
                MapErrorVisibility = true;
                return null;
            }
            var builder = BuilderFactory.GetNewGameBuilder();
            builder.SetDifficulty(DifficultyFactory.GetDifficultyByID(SelectedMap));
            foreach (var player in Players)
            {
                if (String.IsNullOrEmpty(player.Name))
                {
                    player.NameErrorVisibility = true;
                    return null;
                }
                if (player.SelectedRace == -1)
                {
                    player.RaceErrorVisibility = true;
                    return null;
                }
                builder.AddPlayer(player.Name, player.SelectedRace);
            }
            var creator = BuilderFactory.GetGameCreator(builder);
            return creator.CreateGame().GetGame();
        }
    }
}
