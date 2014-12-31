using Model.Map;

namespace ModelTest
{
    public class MockPosition : IPosition
    {
        public int Y
        {
            get { return 2; }
        }

        public int X
        {
            get { return 1; }
        }

        public bool IsAdjacent(IPosition coordinate)
        {
            return false;
        }

        public bool Equals(IPosition other)
        {
            return Y == other.Y && X == other.X;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as IPosition;
            return other != null && Equals(other);
        }
    }
}