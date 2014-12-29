using System.Linq;
using ClassLibrary.Difficulty;
using ClassLibrary.Game;
using ClassLibrary.Game.Builder;
using ClassLibrary.Map;
using ClassLibrary.Race;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var units = game.UnitsAt(new Position(0, 0));
            var unit = units.ElementAt(0);
            game.MoveUnit(unit, new Position(1, 0));
            Assert.AreEqual(1, unit.Position.X);
            Assert.AreEqual(0, unit.Position.Y);
        }
    }
}
