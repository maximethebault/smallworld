using Model.Unit;

namespace Model.Fight.Strategy
{
    public interface IFightStrategy
    {
        IDUnit IDAttacker { get; set; }
        IDUnit IDDefender { get; set; }

        int ComputeTotalRounds();

        float ComputeAttackerWinRatio();
    }
}
