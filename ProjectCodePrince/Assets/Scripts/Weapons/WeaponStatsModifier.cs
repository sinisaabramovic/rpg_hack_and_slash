using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponStatModType
{
    Flat = 100,
    PercentAdd = 200,
    PercentMult = 300,
}

public class WeaponStatsModifier
{

    public readonly float Value;
    public readonly WeaponStatModType statModType;
    public readonly int Order;
    public readonly object Source;

    public WeaponStatsModifier(float value, WeaponStatModType type, int order, object source)
    {
        Value = value;
        statModType = type;
        Order = order;
        Source = source;
    }

    public WeaponStatsModifier(float value, WeaponStatModType type) : this(value, type, (int)type, null) { }

    public WeaponStatsModifier(float value, WeaponStatModType type, int order) : this(value, type, order, null) { }

    public WeaponStatsModifier(float value, WeaponStatModType type, object source) : this(value, type, (int)type, source) { }
}
