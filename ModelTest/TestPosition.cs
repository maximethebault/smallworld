﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Map;

namespace ModelTest
{
    [TestClass]
    public class TestPosition
    {
        [TestMethod]
        public void SameTypeEquality()
        {
            IPosition ipos = new Position(1, 2);
            var pos = new Position(1, 2);
            Assert.AreEqual(ipos, pos);
            Assert.IsTrue(ipos.Equals(pos));
            Assert.IsTrue(pos.Equals(ipos));
        }

        [TestMethod]
        public void DiffTypeEquality()
        {
            IPosition ipos = new MockPosition();
            var pos = new Position(1, 2);
            Assert.IsTrue(ipos.Equals(pos));
            Assert.IsTrue(pos.Equals(ipos));
            var pos1 = new MockPosition();
            var pos2 = new Position(1, 2);
            Assert.IsTrue(pos1.Equals(pos2));
            Assert.IsTrue(pos2.Equals(pos1));
        }

        [TestMethod]
        public void Adjacence()
        {
            var pos1 = new Position(0, 0);
            var pos2 = new Position(0, 1);
            Assert.IsTrue(pos1.IsAdjacent(pos2));
            pos1 = new Position(0, 0);
            pos2 = new Position(1, 0);
            Assert.IsTrue(pos1.IsAdjacent(pos2));
            pos1 = new Position(0, 0);
            pos2 = new Position(1, 1);
            Assert.IsFalse(pos1.IsAdjacent(pos2));

            pos1 = new Position(2, 1);

            pos2 = new Position(2, 0);
            Assert.IsTrue(pos1.IsAdjacent(pos2));
            pos2 = new Position(3, 0);
            Assert.IsTrue(pos1.IsAdjacent(pos2));
            pos2 = new Position(1, 1);
            Assert.IsTrue(pos1.IsAdjacent(pos2));
            pos2 = new Position(3, 1);
            Assert.IsTrue(pos1.IsAdjacent(pos2));
            pos2 = new Position(2, 2);
            Assert.IsTrue(pos1.IsAdjacent(pos2));
            pos2 = new Position(3, 2);
            Assert.IsTrue(pos1.IsAdjacent(pos2));

            pos2 = new Position(2, 1);
            Assert.IsFalse(pos1.IsAdjacent(pos2));
            pos2 = new Position(1, 0);
            Assert.IsFalse(pos1.IsAdjacent(pos2));
            pos2 = new Position(4, 2);
            Assert.IsFalse(pos1.IsAdjacent(pos2));
            pos2 = new Position(4, 0);
            Assert.IsFalse(pos1.IsAdjacent(pos2));
            pos2 = new Position(1, 2);
            Assert.IsFalse(pos1.IsAdjacent(pos2));
            pos2 = new Position(4, 1);
            Assert.IsFalse(pos1.IsAdjacent(pos2));

            pos1 = new Position(2, 2);

            pos2 = new Position(1, 1);
            Assert.IsTrue(pos1.IsAdjacent(pos2));
            pos2 = new Position(2, 1);
            Assert.IsTrue(pos1.IsAdjacent(pos2));
            pos2 = new Position(3, 2);
            Assert.IsTrue(pos1.IsAdjacent(pos2));
            pos2 = new Position(2, 3);
            Assert.IsTrue(pos1.IsAdjacent(pos2));
            pos2 = new Position(1, 3);
            Assert.IsTrue(pos1.IsAdjacent(pos2));
            pos2 = new Position(1, 2);
            Assert.IsTrue(pos1.IsAdjacent(pos2));

            pos2 = new Position(0, 2);
            Assert.IsFalse(pos1.IsAdjacent(pos2));
            pos2 = new Position(3, 1);
            Assert.IsFalse(pos1.IsAdjacent(pos2));
            pos2 = new Position(3, 3);
            Assert.IsFalse(pos1.IsAdjacent(pos2));
            pos2 = new Position(4, 2);
            Assert.IsFalse(pos1.IsAdjacent(pos2));
            pos2 = new Position(1, 0);
            Assert.IsFalse(pos1.IsAdjacent(pos2));
            pos2 = new Position(4, 1);
            Assert.IsFalse(pos1.IsAdjacent(pos2));
        }
    }
}