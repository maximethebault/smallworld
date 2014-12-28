﻿using System;
using ClassLibrary.Map;
using ClassLibrary.Player;

namespace ClassLibrary.Unit
{
    public interface IUnit : IEquatable<IUnit>
    {
        int HealthPoint { get; }
        int AttackPoint { get; }
        int DefensePoint { get; }
        float MovePoint { get; }
        IPosition Position { get; }

        /// <summary>
        /// Equality tests for two Unit instances
        /// </summary>
        /// <param name="other">the Unit to test against</param>
        /// <returns>Whether the given Unit instances are equal (it actually only tests if they're the same instance)</returns>
        new bool Equals(IUnit other);
    }
}