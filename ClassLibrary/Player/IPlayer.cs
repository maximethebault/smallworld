using System.Collections.Generic;
using ClassLibrary.Map;
using ClassLibrary.Race;
using ClassLibrary.Unit;

namespace ClassLibrary.Player
{
    public interface IPlayer
    {
        string Name { get; }
        IRace Race { get; }
        List<IUnit> UnitsAt(IPosition position);
    }
}