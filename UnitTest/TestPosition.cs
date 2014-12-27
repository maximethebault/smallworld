using System;
using ClassLibrary.Map;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    public class MockPosition : IPosition
    {

        public int GetY()
        {
            return 2;
        }

        public int GetX()
        {
            return 1;
        }

        public bool IsAdjacent(IPosition coordinate)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IPosition other)
        {
            return GetY() == other.GetY() && GetX() == other.GetX();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as IPosition;
            return other != null && Equals(other);
        }
    }

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
    }
}
