using ClassLibrary.Unit;

namespace ClassLibrary.Fight
{
    public interface IDFight : IFight
    {
        IDUnit IDAttacker { get; }
        IDUnit IDDefender { get; }
    }
}