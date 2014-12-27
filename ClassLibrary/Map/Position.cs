using System;

namespace ClassLibrary.Map
{
    public class Position : IPosition
    {
        public int Y
        {
            get;
            set;
        }

        public int X
        {
            get;
            set;
        }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int GetY()
        {
            return Y;
        }

        public int GetX()
        {
            return X;
        }

        public bool IsAdjacent(IPosition coordinate)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IPosition other)
        {
            return Y == other.GetY() && X == other.GetX();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as IPosition;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Y*397) ^ X;
            }
        }
    }
}
