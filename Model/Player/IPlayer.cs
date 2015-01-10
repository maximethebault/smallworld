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
        int Score { get; set; }
        List<IUnit> UnitsAt(IPosition position);
    }
}