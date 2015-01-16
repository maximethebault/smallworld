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

namespace UI
{
    class MainWindowViewModel : ViewModelBase
    {
        private static readonly MapDescription[] MapsDescription =
        {
            new MapDescription(new BitmapImage(new Uri("/Images/Map/demo.png", UriKind.Relative)), "Démonstration : 5 tours , 4 unités par peuple"),
            new MapDescription(new BitmapImage(new Uri("/Images/Map/petite.png", UriKind.Relative)), "Petite : 20 tours , 6 unités par peuple"),
            new MapDescription(new BitmapImage(new Uri("/Images/Map/normale.png", UriKind.Relative)), "Standard : 30 tours , 8 unités par peuple")
        };

        private static readonly RaceDescription[] RacesDescription =
        {
            new RaceDescription(new BitmapImage(new Uri("/Images/Race/Creation/elf.png", UriKind.Relative)), "Elf :\nLe coût de déplacement sur une case Forêt est divisé par deux.\nLe coût de déplacement sur une case Désert est multiplié par deux.\nL'unité Elf a une chance sur deux de se replier lors d'un combat perdu, qu'il soit provoqué ou subi, devant normalement conduire à la destruction de l'unité. Dans ce cas l'unité survit avec 1 point de vie."),
            new RaceDescription(new BitmapImage(new Uri("/Images/Race/Creation/nain.png", UriKind.Relative)), "Nain :\nLe coût de déplacement sur une case Plaine est divisé par deux.\nUne unité Nain n'acquière aucun point sur les cases de type Plaine.\nLorsqu'elle se trouve sur une case Montagne, une unité Nain a la capacité de se déplacer sur n'importe quelle case montagne de la carte à condition qu'elle ne soit pas occupée par une unité adverse."),
            new RaceDescription(new BitmapImage(new Uri("/Images/Race/Creation/orc.png", UriKind.Relative)), "Orc :\nLe coût de déplacement sur une case Plaine est divisé par deux.\nL'unité Orc n'acquière aucun point sur les cases de type Forêt.\nLorsqu'une unité Orc détruit une autre unité, elle possède alors 1 point de bonus permanent. Cet effet est cumulable et est lié à chaque unité, c'est-à-dire que si l'unité ayant le bonus meurt, le bonus disparaît.")
        };

        private static readonly BitmapImage[] RacesTextureMap =
        {
            new BitmapImage(new Uri("/Images/Race/Map/elf.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Images/Race/Map/nain.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Images/Race/Map/orc.png", UriKind.Relative))
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
            StartIntro();
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
            var newGame = new GameCreationViewModel(MapsDescription, RacesDescription, PlayerCount);
            newGame.OnNewGame += OnStartGame;
            newGame.OnBackHome += OnBackHome;
            CurrentViewModel = newGame;
        }

        private void StartGame(IGame game)
        {
            var gameCore = new GameCoreViewModel(game, TilesTexture, RacesTextureMap);
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
