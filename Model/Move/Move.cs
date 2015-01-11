using System;

namespace Model.Move
{
    [Serializable()]
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
