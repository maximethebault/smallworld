using Model.Unit;

namespace Model.Turn
{
    public interface IDUnitTurn
    {
        IUnit CurrentUnit { get; }
    }
}