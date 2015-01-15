using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Utils
{
    static class Unit
    {
        public static int ConvertRaceNameToID(string name)
        {
            switch (name)
            {
                case "elf":
                    return 0;
                case "nain":
                    return 1;
                case "orc":
                    return 2;
                default:
                    return -1;
            }
        }
    }
}
