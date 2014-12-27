using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Unit.Exception
{
    class UnitNotFoundException : System.Exception
    {
        public UnitNotFoundException(string message) : base(message)
        {
        }
    }
}
