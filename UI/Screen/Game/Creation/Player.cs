using System;

namespace UI.Screen.Game.Creation
{
    public class Player
    {
        public String Name { get; set; }

        public int Index { get; set; }

        public String PlaceHolder { get; private set; }

        public RaceSelector[] Races { get; set; }

        public Player(int index, PathItem[] races)
        {
            Name = "";
            Index = index;
            PlaceHolder = "Joueur " + (index + 1);
            Races = new RaceSelector[races.Length];
            for (var i = 0; i < races.Length; i++)
            {
                Races[i] = new RaceSelector(races[i], i);
            }
        }
    }
}