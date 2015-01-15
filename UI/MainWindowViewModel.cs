using System;
using System.Windows.Media.Imaging;
using Model.Difficulty;
using Model.Game;
using Model.Game.Builder;
using UI.Screen.Game;
using UI.Screen.Game.Core;
using UI.Screen.Game.Creation;
using UI.Screen.Home;
using UI.Screen.Intro;

// TODO: event unsubscription to avoid memory leak

namespace UI
{
    class MainWindowViewModel : ViewModelBase
    {
        private static readonly BitmapImage[] Maps =
        {
            new BitmapImage(new Uri("/Images/Map/demo.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Images/Map/petite.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Images/Map/normale.png", UriKind.Relative))
        };

        private static readonly BitmapImage[] RacesTexture =
        {
            new BitmapImage(new Uri("/Images/Race/elf.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Images/Race/dwarf.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Images/Race/orc.png", UriKind.Relative))
        };

        private static readonly BitmapImage[] TilesTexture =
        {
            new BitmapImage(new Uri("/Images/Tile/Alt/desert.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Images/Tile/Alt/foret.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Images/Tile/Alt/montagne.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Images/Tile/Alt/plaine.png", UriKind.Relative))
        };

        private const int PlayerCount = 2;

        private ViewModelBase _currentViewModel;

        /// <summary>
        /// ViewModel that is currently bound to the ContentControl
        /// </summary>
        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                RaisePropertyChanged("CurrentViewModel");
            }
        }

        public MainWindowViewModel()
        {
            //var newGameBuilder = BuilderFactory.GetNewGameBuilder();
            //newGameBuilder.AddPlayer("Kikou", 0);
            //newGameBuilder.AddPlayer("salut", 1);
            //newGameBuilder.Difficulty = DifficultyFactory.GetDifficultyByID(0);
            //var gameCreator = BuilderFactory.GetGameCreator(newGameBuilder);
            //var game = gameCreator.CreateGame().GetGame();
            var loadGameBuilder = BuilderFactory.GetLoadGameBuilder();
            loadGameBuilder.SaveFilepath = "C:\\Users\\Max\\Documents\\almostwin.sws";
            var gameCreator = BuilderFactory.GetGameCreator(loadGameBuilder);
            var game = gameCreator.CreateGame().GetGame();
            StartGame(game);
            //StartIntro();
        }

        private void StartIntro()
        {
            var intro = new IntroViewModel();
            intro.OnIntroEnd += OnIntroEnd;
            CurrentViewModel = intro;
        }

        private void StartHome()
        {
            var home = new HomeViewModel();
            home.OnNewGame += OnNewGame;
            home.OnLoadGame += OnLoadGame;
            CurrentViewModel = home;
        }

        private void StartNewGame()
        {
            var newGame = new GameCreationViewModel(Maps, RacesTexture, PlayerCount);
            newGame.OnNewGame += OnStartGame;
            newGame.OnBackHome += OnBackHome;
            CurrentViewModel = newGame;
        }

        private void StartGame(IGame game)
        {
            var gameCore = new GameCoreViewModel(game, TilesTexture, RacesTexture);
            gameCore.OnGameExit += OnGameExit;
            CurrentViewModel = gameCore;
        }

        private void OnIntroEnd(IntroViewModel i, EventArgs e)
        {
            StartHome();
        }

        private void OnBackHome(GameCreationViewModel i, EventArgs e)
        {
            StartHome();
        }

        private void OnNewGame(HomeViewModel i, EventArgs e)
        {
            StartNewGame();
        }

        private void OnLoadGame(HomeViewModel i, GameEventArgs e)
        {
            StartGame(e.Game);
        }

        private void OnStartGame(GameCreationViewModel i, GameEventArgs e)
        {
            StartGame(e.Game);
        }

        private void OnGameExit(GameCoreViewModel t, EventArgs e)
        {
            StartHome();
        }
    }
}
