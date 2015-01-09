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
            var newGameBuilder = BuilderFactory.GetNewGameBuilder();
            newGameBuilder.AddPlayer("Kikou", 1);
            newGameBuilder.AddPlayer("salut", 2);
            newGameBuilder.Difficulty = DifficultyFactory.GetDifficultyByID(1);
            var gameCreator = BuilderFactory.GetGameCreator(newGameBuilder);
            var game = gameCreator.CreateGame().GetGame();
            IEnumerable<IPlayer> actualPlayersList = game.Players;
            Assert.IsFalse(actualPlayersList.IsNullOrEmpty());
            IEnumerable<IPlayer> expectedPlayersList = new List<IPlayer> { new Player("Kikou", new RaceDwarf()), new Player("salut", new RaceOrc()) };
            ComparePlayerList(expectedPlayersList, actualPlayersList);
        }

        [TestMethod]
        [ExpectedException(typeof(TooFewPlayersException))]
        public void PlayerCreationTooFew()
        {
            INewGameBuilder newGameBuilder = BuilderFactory.GetNewGameBuilder();
            newGameBuilder.AddPlayer("Kikou", 1);
            newGameBuilder.AddPlayer("", 2);
            newGameBuilder.AddPlayer("Haha", -1);
            newGameBuilder.AddPlayer(null, 2);
            newGameBuilder.Difficulty = DifficultyFactory.GetDifficultyByID(1);
            var gameCreator = BuilderFactory.GetGameCreator(newGameBuilder);
            var game = gameCreator.CreateGame().GetGame();
        }

        [TestMethod]
        [ExpectedException(typeof(TooManyPlayersException))]
        public void PlayerCreationTooMany()
        {
            INewGameBuilder newGameBuilder = BuilderFactory.GetNewGameBuilder();
            newGameBuilder.AddPlayer("Kikou", 1);
            newGameBuilder.AddPlayer("salut", 2);
            newGameBuilder.AddPlayer("trois", 0);
            newGameBuilder.Difficulty = DifficultyFactory.GetDifficultyByID(1);
            var gameCreator = BuilderFactory.GetGameCreator(newGameBuilder);
            var game = gameCreator.CreateGame().GetGame();
        }

        [TestMethod]
        [ExpectedException(typeof(SameRaceSelectedException))]
        public void PlayerCreationSameRaceSelected()
        {
            INewGameBuilder newGameBuilder = BuilderFactory.GetNewGameBuilder();
            newGameBuilder.AddPlayer("Kikou", 1);
            newGameBuilder.AddPlayer("salut", 1);
            newGameBuilder.Difficulty = DifficultyFactory.GetDifficultyByID(1);
            var gameCreator = BuilderFactory.GetGameCreator(newGameBuilder);
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
        public void MapCreation()
        {
            var difficulty = DifficultyFactory.GetDifficultyByID(1);
            var newGameBuilder = BuilderFactory.GetNewGameBuilder();
            newGameBuilder.AddPlayer("Kikou", 1);
            newGameBuilder.AddPlayer("Mama", 0);
            newGameBuilder.Difficulty = difficulty;
            var gameCreator = BuilderFactory.GetGameCreator(newGameBuilder);
            var game = gameCreator.CreateGame().GetGame();
            var map = game.Map;

            // we're going to check if the number of tiles for each type is well-distributed
            var nbTilesForType = new int[difficulty.NbTileTypes];
            for (var i = 0; i < difficulty.MapWidth; ++i)
            {
                for (var j = 0; j < difficulty.MapWidth; ++j)
                {
                    var tile = map.TileAtPosition(PositionFactory.GetHexaPosition(i, j));
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

            var minNbTilesForType = Math.Floor(((decimal)difficulty.MapWidth * difficulty.MapWidth / difficulty.NbTileTypes));
            var maxNbTilesForType = Math.Ceiling(((decimal)difficulty.MapWidth * difficulty.MapWidth / difficulty.NbTileTypes));

            for (var i = 0; i < difficulty.NbTileTypes; ++i)
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
            var difficulty = DifficultyFactory.GetDifficultyByID(1);
            var newGameBuilder = BuilderFactory.GetNewGameBuilder();
            newGameBuilder.AddPlayer("Kikou", 1);
            newGameBuilder.AddPlayer("Mama", 0);
            newGameBuilder.Difficulty = difficulty;
            var gameCreator = BuilderFactory.GetGameCreator(newGameBuilder);
            var game = (Game) gameCreator.CreateGame().GetGame();
            var players = game.IDPlayers;
            var playerNo = 0;
            using (var playersIterator = players.GetEnumerator())
            {
                while (playersIterator.MoveNext())
                {
                    var player = playersIterator.Current;
                    var units = player.IDUnits;
                    Assert.AreEqual(difficulty.NbUnitsPerRace, units.Count);
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
                                Assert.AreEqual(difficulty.MapWidth-1, unitPosition.X);
                                Assert.AreEqual(difficulty.MapWidth - 1, unitPosition.Y);
                            }
                        }
                    }
                    playerNo++;
                }
            }
        }
    }
}
