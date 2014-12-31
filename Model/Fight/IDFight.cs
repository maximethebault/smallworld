using Model.Unit;

namespace Model.Fight
{
    public interface IDFight : IFight
    {
        IDUnit IDAttacker { get; }
        IDUnit IDDefender { get; }
    }
}