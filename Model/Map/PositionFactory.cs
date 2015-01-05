using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Map
{
    class PositionFactory
    {
        public static IPosition GetHexaPosition(int x, int y)
        {
            return new HexaPosition(x, y);
        }
    }
}
