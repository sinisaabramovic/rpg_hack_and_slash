﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatModType
{
    Flat = 100,
    PercentAdd = 200,
    PercentMult = 300,
}

public class StatsModifier
{

    public readonly float Value;
    public readonly StatModType statModType;
    public readonly int Order;
    public readonly object Source;

    public StatsModifier(float value, StatModType type, int order, object source)
    {
        Value = value;
        statModType = type;
        Order = order;
        Source = source;
    }

    public StatsModifier(float value, StatModType type) : this(value, type, (int)type, null) { }

    public StatsModifier(float value, StatModType type, int order) : this(value, type, order, null) { }

    public StatsModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }
}
