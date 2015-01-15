using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Difficulty;
using Model.Fight;
using Model.Fight.Strategy;
using Model.Game;
using Model.Game.Builder;
using Model.Map;
using Model.Move;
using Model.Player;
using Model.Race;
using Model.Tile;
using Model.Unit;
using Model.Unit.Exception;

namespace ModelTest
{
    [TestClass]
    public class TestUnit
    {
        [TestMethod]
        public void MoveUnit()
        {
            var difficulty = DifficultyFactory.GetDifficultyByID(1);
            INewGameBuilder newGameBuilder = BuilderFactory.GetNewGameBuilder();
            newGameBuilder.AddPlayer("Kikou", 1);
            newGameBuilder.AddPlayer("Mama", 0);
            newGameBuilder.Difficulty = difficulty;
            var gameCreator = BuilderFactory.GetGameCreator(newGameBuilder);
            var game = (Game) gameCreator.CreateGame().GetGame();
            var units = game.UnitsAt(PositionFactory.GetHexaPosition(0, 0));
            var unit = units.ElementAt(0);
            game.MoveUnit(unit, PositionFactory.GetHexaPosition(1, 0));
            Assert.AreEqual(1, unit.Position.X);
            Assert.AreEqual(0, unit.Position.Y);
        }

        [TestMethod]
        [ExpectedException(typeof(UnitMovementUnauthorized))]
        public void MoveUnitUnauthorized()
        {
            var difficulty = DifficultyFactory.GetDifficultyByID(1);
            INewGameBuilder newGameBuilder = BuilderFactory.GetNewGameBuilder();
            newGameBuilder.AddPlayer("Kikou", 1);
            newGameBuilder.AddPlayer("Mama", 0);
            newGameBuilder.Difficulty = difficulty;
            var gameCreator = BuilderFactory.GetGameCreator(newGameBuilder);
            var game = (Game)gameCreator.CreateGame().GetGame();
            var units = game.UnitsAt(PositionFactory.GetHexaPosition(0, 0));
            var unit = units.ElementAt(0);
            game.MoveUnit(unit, PositionFactory.GetHexaPosition(2, 0));
            Assert.AreEqual(1, unit.Position.X);
            Assert.AreEqual(0, unit.Position.Y);
        }

        [TestMethod]
        [ExpectedException(typeof(UnitNotFoundException))]
        public void MoveUnexistingUnit()
        {
            var difficulty = DifficultyFactory.GetDifficultyByID(1);
            INewGameBuilder newGameBuilder = BuilderFactory.GetNewGameBuilder();
            newGameBuilder.AddPlayer("Kikou", 1);
            newGameBuilder.AddPlayer("Mama", 0);
            newGameBuilder.Difficulty = difficulty;
            var gameCreator = BuilderFactory.GetGameCreator(newGameBuilder);
            var game = (Game)gameCreator.CreateGame().GetGame();
            IUnit unit = new UnitDwarf(new Player("Ha", new RaceDwarf()), PositionFactory.GetHexaPosition(1, 2), TileFlyweightFactory.CreateTile(0));
            game.MoveUnit(unit, PositionFactory.GetHexaPosition(1, 0));
            Assert.AreEqual(1, unit.Position.X);
            Assert.AreEqual(0, unit.Position.Y);
        }

        [TestMethod]
        public void MoveUnitSimple()
        {
            var difficulty = DifficultyFactory.GetDifficultyByID(1);
            INewGameBuilder newGameBuilder = BuilderFactory.GetNewGameBuilder();
            newGameBuilder.AddPlayer("Kikou", 2);
            newGameBuilder.AddPlayer("Mama", 1);
            newGameBuilder.Difficulty = difficulty;
            var gameCreator = BuilderFactory.GetGameCreator(newGameBuilder);
            var game = (Game)gameCreator.CreateGame().GetGame();
            var units = game.UnitsAt(PositionFactory.GetHexaPosition(0, 0));
            var unit = (Unit) units.ElementAt(0);
            IMove move;
            for (var i = 1; i < difficulty.MapWidth-1; i++)
            {
                move = game.MoveUnit(unit, PositionFactory.GetHexaPosition(i, i-1));
                Assert.IsTrue(move.Success);
                Assert.IsFalse(move.Fight);
                Assert.AreEqual(i, unit.Position.X);
                Assert.AreEqual(i-1, unit.Position.Y);
                unit.ResetMovePoint();
                move = game.MoveUnit(unit, PositionFactory.GetHexaPosition(i, i));
                Assert.IsTrue(move.Success);
                Assert.IsFalse(move.Fight);
                Assert.AreEqual(i, unit.Position.X);
                Assert.AreEqual(i, unit.Position.Y);
                unit.ResetMovePoint();
            }
            move = game.MoveUnit(unit, PositionFactory.GetHexaPosition(difficulty.MapWidth - 1, difficulty.MapWidth - 2));
            Assert.IsTrue(move.Success);
            Assert.IsFalse(move.Fight);
            unit.ResetMovePoint();
            move = game.MoveUnit(unit, PositionFactory.GetHexaPosition(difficulty.MapWidth - 1, difficulty.MapWidth - 1));
            Assert.IsFalse(move.Success);
            Assert.IsTrue(move.Fight);
            var fight = (Fight) game.IDFight;
            var initHealthPoint = unit.InitialHealthPoint;
            fight.FightStrategy = new DeterministicFightStrategy(20, 1);
            var losingPlayer = fight.IDDefender.IDPlayer;
            var nbUnits = losingPlayer.IDUnits.Count;
            Assert.IsFalse(fight.Finished);
            for (var i = 0; i < initHealthPoint; i++)
            {
                Assert.AreEqual(initHealthPoint, fight.IDAttacker.HealthPoint);
                Assert.AreEqual(initHealthPoint - i, fight.IDDefender.HealthPoint);
                game.NextFightRound();
            }
            Assert.IsTrue(fight.Finished);
            Assert.AreEqual(null, game.IDFight);
            Assert.AreEqual(nbUnits - 1, losingPlayer.IDUnits.Count);
            if (fight.IDAttacker.Tile.IsForest())
            {
                Assert.AreEqual(0, fight.IDAttacker.ComputeScore());
            }
            else
            {
                Assert.AreEqual(2, fight.IDAttacker.ComputeScore());
            }
            Assert.AreEqual(difficulty.MapWidth - 1, fight.IDAttacker.Position.X);
            Assert.AreEqual(difficulty.MapWidth - 2, fight.IDAttacker.Position.Y);
        }
    }
}
