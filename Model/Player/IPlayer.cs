using System.Collections.Generic;
using Model.Map;
using Model.Race;
using Model.Unit;

namespace Model.Player
{
    public interface IPlayer
    {
        string Name { get; }
        IRace Race { get; }
        List<IUnit> UnitsAt(IPosition position);
    }
}