using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI.Screen.Game.Creation
{
    /// <summary>
    /// Logique d'interaction pour GameCreation.xaml
    /// </summary>
    public partial class GameCreation : UserControl
    {
        public PathItem[] Maps { get; set; }

        private PathItem[] Races { get; set; }

        public List<Player> Players { get; set; }

        public GameCreation(PathItem[] maps, PathItem[] races, int playerCount)
        {
            Maps = maps;
            Races = races;
            Players = new List<Player>();
            for (var i = 0; i < playerCount; i++)
            {
                Players.Add(new Player(i, Races));
            }
            InitializeComponent();
        }

        private void OnClickNext(object sender, MouseButtonEventArgs e)
        {
            var selectedMap = -1;
            for (var i = 0; i < MapListBox.SelectedItems.Count; i++)
            {
                selectedMap = i;
                break;
            }
        }

        private void OnRaceSelection(object sender, SelectionChangedEventArgs e)
        {
            var listbox = sender as RaceListBox;
            if (listbox == null) return;
            var currentPlayer = listbox.DataContext as Player;
            RaceSelector unselected = null;
            if (e.RemovedItems.Count > 0)
            {
                unselected = e.RemovedItems[0] as RaceSelector;
            }
            RaceSelector selected = null;
            if (e.AddedItems.Count > 0)
            {
                selected = e.AddedItems[0] as RaceSelector;
            }
            foreach(var player in Players.Where(player => !Object.ReferenceEquals(player, currentPlayer)))
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
    }
}
