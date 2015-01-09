using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Model.Game.Exception;
using Model.Map;
using Model.Player;
using Model.Race;
using Model.Tile;
using Wrapper;

namespace Model.Game.Builder
{
    class NewGameBuilder : GameBuilder, INewGameBuilder
    {
        private readonly List<String> _playerNames;
        private readonly List<IDRace> _playerRaces;
        private WrapperAlgo _wrapperAlgo;

        public NewGameBuilder()
        {
            _playerRaces = new List<IDRace>();
            _playerNames = new List<string>();
        }

        public override void Create()
        {
            base.Create();
            _wrapperAlgo = new WrapperAlgo(Difficulty.GetMapWidth(), _playerNames.Count, Difficulty.GetNbTileTypes());
        }

        public override void BuildPlayer()
        {
            var difficulty = IDGame.DifficultyStrategy;
            if (_playerNames.Count > difficulty.GetMaxPlayer())
                throw new TooManyPlayersException("Cette carte ne supporte pas plus de " + difficulty.GetMaxPlayer() + " joueurs, " + _playerNames.Count + " donnés");
            if (_playerNames.Count < difficulty.GetMinPlayer())
                throw new TooFewPlayersException("Cette carte nécessite au moins " + difficulty.GetMinPlayer() + " joueurs, " + _playerNames.Count + " donné(s)");

            var players = new List<IDPlayer>();
            var alreadyUsedRaces = new List<String>();

            using (var playerNamesIterator = _playerNames.GetEnumerator())
            using (var playerRacesIterator = _playerRaces.GetEnumerator())
            {
                while (playerNamesIterator.MoveNext() && playerRacesIterator.MoveNext())
                {
                    var name = playerNamesIterator.Current;
                    var race = playerRacesIterator.Current;
                    if (alreadyUsedRaces.IndexOf(race.GetName()) == -1)
                    {
                        alreadyUsedRaces.Add(race.GetName());
                        players.Add(new Player.Player(name, race));
                    }
                    else
                    {
                        throw new SameRaceSelectedException("The race " + race .GetName() + " was already selected by another player!");
                    }
            }
            }
            players.Shuffle();
            IDGame.IDPlayers = players;
            IDGame.PlayerTurnOrder = IDGame.IDPlayers.GetEnumerator();
            IDGame.PlayerTurnOrder.MoveNext();
        }

        public unsafe override void BuildUnits()
        {
            var difficulty = IDGame.DifficultyStrategy;
            var players = _wrapperAlgo.placePlayers();
            for (var i = 0; i < IDGame.Players.Count; ++i)
            {
                var player = IDGame.IDPlayers.ElementAt(i);
                for (var j = 0; j < difficulty.GetNbUnitsPerRace(); ++j)
                {
                    var position = PositionFactory.GetHexaPosition(players[i][0], players[i][1]);
                    var unit = player.IDRace.CreateUnit(player, position, IDGame.Map.TileAtPosition(position));
                    player.IDUnits.Add(unit);
                }
            }
        }

        public unsafe override void BuildMap()
        {
            var difficulty = IDGame.DifficultyStrategy;
            var map = new Map.Map(difficulty.GetMapWidth());
            var mapTiles = _wrapperAlgo.createMap();

            for (var i = 0; i < difficulty.GetMapWidth(); ++i)
            {
                for (var j = 0; j < difficulty.GetMapWidth(); ++j)
                {
                    var tileType = mapTiles[i][j]++;
                    map.AddTile(PositionFactory.GetHexaPosition(i, j), TileFlyweightFactory.CreateTile(tileType));
                }
            }

            IDGame.IDMap = map;
        }

        public override void BuildDifficulty()
        {
            IDGame.DifficultyStrategy = Difficulty;
        }

        public void AddPlayer(string name, int raceID)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            var race = RaceFactory.GetRaceByID(raceID);
            if (race == null)
            {
                return;
            }
            _playerNames.Add(name);
            _playerRaces.Add(race);
        }
    }

    static class ListExtension
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            var provider = new RNGCryptoServiceProvider();
            var n = list.Count;
            while (n > 1)
            {
                var box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                var k = (box[0] % n);
                n--;
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
