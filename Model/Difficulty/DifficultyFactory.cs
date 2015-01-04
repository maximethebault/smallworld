using System;

namespace Model.Difficulty
{
    public class DifficultyFactory
    {
        public enum DifficultyID
        {
            Demo = 0,
            Small,
            Standard
        }

        public static IDifficultyStrategy GetDifficultyByID(int id)
        {
            switch ((DifficultyID) id)
            {
                case DifficultyID.Demo:
                    return new DemoMapStrategy();
                case DifficultyID.Small:
                    return new SmallMapStrategy();
                case DifficultyID.Standard:
                    return new StandardMapStrategy();
                default:
                    return null;

            }
        }
    }
}
