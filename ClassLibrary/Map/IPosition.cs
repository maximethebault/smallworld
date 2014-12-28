using System;

namespace ClassLibrary.Map
{
    public interface IPosition : IEquatable<IPosition>
    {
        int Y { get; }
        int X { get; }
        bool IsAdjacent(IPosition coordinate);
        new bool Equals(IPosition pos);
    }
}