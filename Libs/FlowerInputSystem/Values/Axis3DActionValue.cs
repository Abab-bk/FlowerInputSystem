﻿using System.Numerics;

namespace FlowerInputSystem.Values;

public struct Axis3DActionValue : IActionValue
{
    public Vector3 Value { get; set; }

    public ValueDimension Dimension { get; }

    public bool IsActuated(float actuation)
    {
        throw new NotImplementedException();
    }
}