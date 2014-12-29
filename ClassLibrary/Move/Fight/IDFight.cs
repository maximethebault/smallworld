using ClassLibrary.Unit;

namespace ClassLibrary.Move.Fight
{
    public interface IDFight : IFight
    {
        IDUnit IDAttacker { get; }
        IDUnit IDDefender { get; }
    }
}