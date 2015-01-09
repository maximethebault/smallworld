using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Practices.Prism.Commands;
using Model.Game;
using Model.Map;
using UI.Screen.Game.Core.Unit.ViewModel;
using UI.Utils;

namespace UI.Screen.Game.Core.Map.ViewModel
{
    public class MapViewModel : ViewModelBase
    {
        public event MoveToTileEventHandler OnMoveToTile;
        public delegate void MoveToTileEventHandler(TileViewModel t, EventArgs e);

        public event SelectTileEventHandler OnSelectTile;
        public delegate void SelectTileEventHandler(TileViewModel t, EventArgs e);


        public BitmapImage[] TilesTexture { get; set; }

        public BitmapImage[] RacesTexture { get; set; }

        public IGame Game { get; set; }

        public ObservableCollection<TileViewModel> Tiles { get; set; }

        private TileViewModel _selectedTile;

        public TileViewModel SelectedTile
        {
            get { return _selectedTile; }
            set
            {
                _selectedTile = value;
                RaisePropertyChanged("SelectedTile");
            }
        }

        public ICommand SelectTileCommand { get; private set; }
        public ICommand MoveToTileCommand { get; private set; }

        public MapViewModel(IGame game, BitmapImage[] tilesTexture, BitmapImage[] racesTexture)
        {
            Game = game;
            TilesTexture = tilesTexture;
            RacesTexture = racesTexture;

            Tiles = new ObservableCollection<TileViewModel>();

            SelectTileCommand = new DelegateCommand<TileViewModel>(SelectTile);
            MoveToTileCommand = new DelegateCommand<TileViewModel>(MoveToTile);

            StartLoading();
        }

        public void StartLoading()
        {
            var difficulty = Game.DifficultyStrategy;
            for (var i = 0; i < difficulty.MapWidth; ++i) // i -> y
            {
                for (var j = 0; j < difficulty.MapWidth; ++j) // j -> x
                {
                    var tileObject = Game.Map.TileAtPosition(PositionFactory.GetHexaPosition(i, j));
                    var tileTextureIndex = 0;
                    if (tileObject.IsDesert())
                    {
                        tileTextureIndex = 0;
                    }
                    else if (tileObject.IsForest())
                    {
                        tileTextureIndex = 1;
                    }
                    else if (tileObject.IsMountain())
                    {
                        tileTextureIndex = 2;
                    }
                    else if (tileObject.IsPlain())
                    {
                        tileTextureIndex = 3;
                    }
                    var tile = new TileViewModel { Column = i, Row = j, Texture = TilesTexture[tileTextureIndex] };
                    Tiles.Add(tile);
                }
            }
            UpdateUnitOnTile();
        }

        public int ConvertRaceNameToID(string name)
        {
            switch (name)
            {
                case "elf":
                    return 0;
                case "nain":
                    return 1;
                case "orc":
                    return 2;
                default:
                    return -1;
            }
        }

        public void UpdateUnitOnTile()
        {
            foreach (var tile in Tiles)
            {
                var units = Game.UnitsAt(PositionFactory.GetHexaPosition(tile.Column, tile.Row));
                var unitCollection = new List<UnitViewModel>();
                var i = 0;
                foreach (var unitModel in units)
                {
                    var unitViewModel = tile.Units.FirstOrDefault(vm => vm.Model.Equals(unitModel)) ?? new UnitViewModel(Game, unitModel) {Row = i};
                    unitCollection.Add(unitViewModel);
                    i++;
                }

                tile.Units.Sync(unitCollection);
                if (!tile.HasUnit)
                {
                    tile.UnitTexture = null;
                    continue;
                }

                var unit = tile.Units.First();
                var raceID = ConvertRaceNameToID(unit.Model.Player.Race.GetName());
                tile.UnitTexture = raceID == -1 ? null : RacesTexture[raceID];
            }
        }

        private void SelectTile(TileViewModel tileViewModel)
        {
            if (SelectedTile != null)
            {
                SelectedTile.IsSelected = false;
            }
            SelectedTile = tileViewModel;
            if (SelectedTile != null)
            {
                SelectedTile.IsSelected = true;
            }

            if (OnSelectTile != null)
            {
                OnSelectTile(tileViewModel, null);
            }
        }

        private void MoveToTile(TileViewModel tileViewModel)
        {
            if (tileViewModel == null || SelectedTile == null)
            {
                return;
            }
            if (OnMoveToTile == null)
            {
                return;
            }
            OnMoveToTile(tileViewModel, null);
        }
    }
}
