using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Difficulty;
using Model.Game;
using Model.Game.Builder;
using Model.Game.Exception;
using Model.Map;
using Model.Player;
using Model.Race;
using Wrapper;

namespace ModelTest
{
    [TestClass]
    public class TestGameBuilding
    {
        [TestMethod]
        public void PlayerCreation()
        {
            INewGameBuilder newGameBuilder = new NewGameBuilder();
            newGameBuilder.AddPlayer("Kikou", new RaceDwarf());
            newGameBuilder.AddPlayer("salut", new RaceOrc());
            newGameBuilder.SetDifficulty(new SmallMapStrategy());
            var gameCreator = new GameCreator(newGameBuilder);
            var game = gameCreator.CreateGame().GetGame();
            IEnumerable<IPlayer> actualPlayersList = game.Players;
            Assert.IsFalse(actualPlayersList.IsNullOrEmpty());
            IEnumerable<IPlayer> expectedPlayersList = new List<IPlayer> { (IPlayer)new Player("Kikou", new RaceDwarf()), (IPlayer)new Player("salut", new RaceOrc()) };
            ComparePlayerList(expectedPlayersList, actualPlayersList);
        }

        [TestMethod]
        [ExpectedException(typeof(TooFewPlayersException))]
        public void PlayerCreationTooFew()
        {
            INewGameBuilder newGameBuilder = new NewGameBuilder();
            newGameBuilder.AddPlayer("Kikou", new RaceDwarf());
            newGameBuilder.AddPlayer("", new RaceElf());
            newGameBuilder.AddPlayer("Haha", null);
            newGameBuilder.AddPlayer(null, new RaceElf());
            newGameBuilder.SetDifficulty(new SmallMapStrategy());
            var gameCreator = new GameCreator(newGameBuilder);
            var game = gameCreator.CreateGame().GetGame();
        }

        [TestMethod]
        [ExpectedException(typeof(TooManyPlayersException))]
        public void PlayerCreationTooMany()
        {
            INewGameBuilder newGameBuilder = new NewGameBuilder();
            newGameBuilder.AddPlayer("Kikou", new RaceDwarf());
            newGameBuilder.AddPlayer("salut", new RaceOrc());
            newGameBuilder.AddPlayer("trois", new RaceElf());
            newGameBuilder.SetDifficulty(new SmallMapStrategy());
            var gameCreator = new GameCreator(newGameBuilder);
            var game = gameCreator.CreateGame().GetGame();
        }

        [TestMethod]
        [ExpectedException(typeof(SameRaceSelectedException))]
        public void PlayerCreationSameRaceSelected()
        {
            INewGameBuilder newGameBuilder = new NewGameBuilder();
            newGameBuilder.AddPlayer("Kikou", new RaceDwarf());
            newGameBuilder.AddPlayer("salut", new RaceDwarf());
            newGameBuilder.SetDifficulty(new SmallMapStrategy());
            var gameCreator = new GameCreator(newGameBuilder);
            var game = gameCreator.CreateGame().GetGame();
        }

        public void ComparePlayerList(IEnumerable<IPlayer> expectedPlayers, IEnumerable<IPlayer> actualPlayers)
        {
            using (var expectedPlayersIterator = expectedPlayers.GetEnumerator())
            using (var actualPlayersIterator = actualPlayers.GetEnumerator())
            {
                while (expectedPlayersIterator.MoveNext() && actualPlayersIterator.MoveNext())
                {
                    var expectedPlayer = expectedPlayersIterator.Current;
                    var actualPlayer = actualPlayersIterator.Current;
                    Assert.AreEqual(expectedPlayer.Name, actualPlayer.Name);
                    Assert.AreEqual(expectedPlayer.Race.GetName(), actualPlayer.Race.GetName());
                }
            }
        }

        [TestMethod]
        public unsafe void WrapperMapCreation()
        {
            var mapSize = 30;
            var nbTypes = 4;
            var wrapper = new WrapperAlgo(mapSize, 2, nbTypes);
            // TODO: faire un test avec WrapperAlgo, voir si le destructeur est bien appelé à la fin d'un bloc local
            var map = wrapper.createMap();
            // we're going to check if the number of tiles for each type is well-distributed
            var nbTilesForType = new int[nbTypes];
            for (var i = 0; i < mapSize; ++i)
            {
                for (var j = 0; j < mapSize; ++j)
                {
                    nbTilesForType[map[i][j]]++;
                }
            }
            var minNbTilesForType = Math.Floor(((decimal)mapSize * mapSize / nbTypes));
            var maxNbTilesForType = Math.Ceiling(((decimal)mapSize * mapSize / nbTypes));

            for (var i = 0; i < nbTypes; ++i)
            {
                Assert.IsTrue(nbTilesForType[i] >= minNbTilesForType);
                Assert.IsTrue(nbTilesForType[i] <= maxNbTilesForType);
            }
        }

        [TestMethod]
        public unsafe void MapCreation()
        {
            var difficulty = new SmallMapStrategy();
            INewGameBuilder newGameBuilder = new NewGameBuilder();
            newGameBuilder.AddPlayer("Kikou", new RaceDwarf());
            newGameBuilder.AddPlayer("Mama", new RaceElf());
            newGameBuilder.SetDifficulty(difficulty);
            var gameCreator = new GameCreator(newGameBuilder);
            var game = gameCreator.CreateGame().GetGame();
            var map = game.Map;

            // we're going to check if the number of tiles for each type is well-distributed
            var nbTilesForType = new int[difficulty.GetNbTileTypes()];
            for (var i = 0; i < difficulty.GetMapWidth(); ++i)
            {
                for (var j = 0; j < difficulty.GetMapWidth(); ++j)
                {
                    var tile = map.TileAtPosition(new Position(i, j));
                    if (tile.IsDesert())
                    {
                        nbTilesForType[0]++;
                    }
                    else if (tile.IsForest())
                    {
                        nbTilesForType[1]++;
                    }
                    else if (tile.IsMountain())
                    {
                        nbTilesForType[2]++;
                    }
                    else if (tile.IsPlain())
                    {
                        nbTilesForType[3]++;
                    }
                    else
                    {
                        throw new Exception("More tile types than expected for running this test!");
                    }
                }
            }

            var minNbTilesForType = Math.Floor(((decimal)difficulty.GetMapWidth() * difficulty.GetMapWidth() / difficulty.GetNbTileTypes()));
            var maxNbTilesForType = Math.Ceiling(((decimal)difficulty.GetMapWidth() * difficulty.GetMapWidth() / difficulty.GetNbTileTypes()));

            for (var i = 0; i < difficulty.GetNbTileTypes(); ++i)
            {
                Assert.IsTrue(nbTilesForType[i] >= minNbTilesForType);
                Assert.IsTrue(nbTilesForType[i] <= maxNbTilesForType);
            }
        }

        public unsafe void PlaceNPlayers(int nbPlayers)
        {
            var mapSize = 30;
            var wrapper = new WrapperAlgo(mapSize, nbPlayers, 4);
            var playerPlacement = wrapper.placePlayers();
            // we're going to check if the players are well placed (as far away as possible from each other)
            Assert.AreEqual(0, playerPlacement[0][0]);
            Assert.AreEqual(0, playerPlacement[0][1]);

            Assert.AreEqual(mapSize-1, playerPlacement[1][0]);
            Assert.AreEqual(mapSize-1, playerPlacement[1][1]);

            
            if (nbPlayers <= 2) return;

            Assert.AreEqual(0, playerPlacement[2][0]);
            Assert.AreEqual(mapSize - 1, playerPlacement[2][1]);

            if (nbPlayers <= 3) return;

            Assert.AreEqual(mapSize - 1, playerPlacement[3][0]);
            Assert.AreEqual(0, playerPlacement[3][1]);
        }

        [TestMethod]
        public void WrapperPlacePlayers()
        {
            PlaceNPlayers(2);
            PlaceNPlayers(3);
            PlaceNPlayers(4);
        }

        [TestMethod]
        public void PlacePlayers()
        {
            // we're only going to test a 2-players game, because there are never more!
            var difficulty = new SmallMapStrategy();
            INewGameBuilder newGameBuilder = new NewGameBuilder();
            newGameBuilder.AddPlayer("Kikou", new RaceDwarf());
            newGameBuilder.AddPlayer("Mama", new RaceElf());
            newGameBuilder.SetDifficulty(difficulty);
            var gameCreator = new GameCreator(newGameBuilder);
            var game = (Game) gameCreator.CreateGame().GetGame();
            var players = game.IDPlayers;
            var playerNo = 0;
            using (var playersIterator = players.GetEnumerator())
            {
                while (playersIterator.MoveNext())
                {
                    var player = playersIterator.Current;
                    var units = player.IDUnits;
                    Assert.AreEqual(difficulty.GetNbUnitsPerRace(), units.Count);
                    using (var unitsIterator = units.GetEnumerator())
                    {
                        while (unitsIterator.MoveNext())
                        {
                            var unit = unitsIterator.Current;
                            var unitPosition = unit.Position;
                            if (playerNo == 0)
                            {
                                Assert.AreEqual(0, unitPosition.X);
                                Assert.AreEqual(0, unitPosition.Y);
                            }
                            else
                            {
                                Assert.AreEqual(difficulty.GetMapWidth()-1, unitPosition.X);
                                Assert.AreEqual(difficulty.GetMapWidth() - 1, unitPosition.Y);
                            }
                        }
                    }
                    playerNo++;
                }
            }
        }
    }
}
