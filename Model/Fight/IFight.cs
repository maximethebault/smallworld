using Model.Unit;

namespace Model.Fight
{
    public interface IFight
    {
        int ElapsedRounds { get; }
        int TotalRounds { get; }
        IUnit Attacker { get; }
        IUnit Defender { get; }
        IUnit Winner { get; }
        IUnit Loser { get; }
        void NextRound();
        bool IsFinished();
    }
}