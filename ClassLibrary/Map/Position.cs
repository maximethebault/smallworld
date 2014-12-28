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

        public bool IsAdjacent(IPosition other)
        {
            /*
             * A tile looks like this:
             * 1  /\  4
             * 2 |  | 5
             * 3  \/  6
             * 
             * There are 6 possible neighboors.
             * 
             * It's easy to know the indexes of 2 and 5, but more complicated to know 1, 4, 3 and 6.
             * 
             * Indeed, tiles are hexagonal, which implies they can't all start on the same container's border, as show below:
             * |  /\
             * | |  |
             * |  \//\
             * |   |  |
             * |  /\\/
             * | |  |
             * |  \/
             * 
             * Odd rows start with an offset, while even rows haven't got any
             */
            
            // neighboors 2, 5: same row, difference on column index is 1
            if (Y == other.Y && Math.Abs(X - other.X) == 1)
            {
                return true;
            }
            // neihgboors 1, 4, 3, 6 ; 'this' tile is on even row
            if (Y % 2 == 0 && Math.Abs(Y - other.Y) == 1 && (X == other.X || X == other.X + 1))
            {
                return true;
            }
            // neihgboors 1, 4, 3, 6 ; 'this' tile is on odd row
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (Y % 2 == 1 && Math.Abs(Y - other.Y) == 1 && (X == other.X || X == other.X - 1))
            {
                return true;
            }
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

        public override int GetHashCode()
        {
            unchecked
            {
                return (Y*397) ^ X;
            }
        }
    }
}
