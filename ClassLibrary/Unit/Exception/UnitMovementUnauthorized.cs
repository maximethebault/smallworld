﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Unit.Exception
{
    class UnitMovementUnauthorized : System.Exception
    {
        public UnitMovementUnauthorized(string message) : base(message)
        {
        }
    }
}