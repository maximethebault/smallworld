namespace Model.Race
{
    public class RaceFactory
    {
        public enum RaceID
        {
            Elf = 0,
            Dwarf,
            Orc
        }

        public static IDRace GetRaceByID(int id)
        {
            switch ((RaceID)id)
            {
                case RaceID.Elf:
                    return new RaceElf();
                case RaceID.Dwarf:
                    return new RaceDwarf();
                case RaceID.Orc:
                    return new RaceOrc();
                default:
                    return null;

            }
        }
    }
}
