using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Move
{
    class Move : IMove
    {
        public bool Success { get; set; }
        public bool Fight { get; set; }

        public Move()
        {
            Success = false;
            Fight = false;
        }
    }
}
