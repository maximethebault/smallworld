using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using ClassLibrary;
using ClassLibrary.Difficulty;
using ClassLibrary.GameEngine;
using ClassLibrary.GameEngine.Builder;
using ClassLibrary.GameEngine.Exception;
using ClassLibrary.Map;
using ClassLibrary.Player;
using ClassLibrary.Race;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wrapper;

namespace UnitTest
{
    [TestClass]
    public class TestUnit
    {
        [TestMethod]
        public void MoveUnit()
        {
            var difficulty = new SmallMapStrategy();
            INewGameBuilder newGameBuilder = new NewGameBuilder();
            newGameBuilder.AddPlayer("Kikou", new RaceDwarf());
            newGameBuilder.AddPlayer("Mama", new RaceElf());
            newGameBuilder.SetDifficulty(difficulty);
            var gameCreator = new GameCreator(newGameBuilder);
            var game = (Game) gameCreator.CreateGame().GetGame();
            var units = game.GetUnitsAt(new Position(0, 0));
            var unit = units.ElementAt(0);
            game.MoveUnit(unit, new Position(1, 0));
        }
    }
}
