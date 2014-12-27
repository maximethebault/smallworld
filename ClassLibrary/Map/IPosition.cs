using System;

namespace ClassLibrary.Map
{
    public interface IPosition : IEquatable<IPosition>
    {
        int GetY();
        int GetX();
        bool IsAdjacent(IPosition coordinate);
        new bool Equals(IPosition pos);
    }
}