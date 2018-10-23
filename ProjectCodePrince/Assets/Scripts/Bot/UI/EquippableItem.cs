using UnityEngine;
using Sino.CharacterStats;

public enum EquipmentType{
    Helmet,
    Chest1,
    Chest2,
    Gloves,
    Boots,
    Weapon1,
    Weapon2,
    Accessories1,
    Accessories2,
}

[CreateAssetMenu]
public class EquippableItem : Item {

    public int StrengthBonus;
    public int AgilityBonus;
    public int IntelligenceBonus;
    public int VitalityBonus;
    [Space]
    public int StrengthPercentBonus;
    public int AgilityPercentBonus;
    public int IntelligencePercentBonus;
    public int VitalityPercentBonus;
    [Space]
    public EquipmentType equipmentType;

    public void Equip(Character characterStat){        
        if(StrengthBonus != 0)
        {          
            characterStat.Strength.AddModifier(new StatsModifier(StrengthBonus, StatModType.Flat, this));
        }
        if (AgilityBonus != 0)
        {
            characterStat.Agility.AddModifier(new StatsModifier(AgilityBonus, StatModType.Flat, this));
        }
        if (IntelligenceBonus != 0)
        {
            characterStat.Intelligence.AddModifier(new StatsModifier(IntelligenceBonus, StatModType.Flat, this));
        }
        if (VitalityBonus != 0)
        {
            characterStat.Vitality.AddModifier(new StatsModifier(VitalityBonus, StatModType.Flat, this));
        }

        if (StrengthPercentBonus != 0)
        {
            characterStat.Strength.AddModifier(new StatsModifier(StrengthPercentBonus, StatModType.PercentMult, this));
        }
        if (AgilityPercentBonus != 0)
        {
            characterStat.Agility.AddModifier(new StatsModifier(AgilityPercentBonus, StatModType.PercentMult, this));
        }
        if (IntelligencePercentBonus != 0)
        {
            characterStat.Intelligence.AddModifier(new StatsModifier(IntelligencePercentBonus, StatModType.PercentMult, this));
        }
        if (VitalityPercentBonus != 0)
        {
            characterStat.Vitality.AddModifier(new StatsModifier(VitalityPercentBonus, StatModType.PercentMult, this));
        }
    }

    public void Unequip(Character characterStat)
    {
        characterStat.Strength.RemoveAllModifiersFromSource(this);
        characterStat.Agility.RemoveAllModifiersFromSource(this);
        characterStat.Intelligence.RemoveAllModifiersFromSource(this);
        characterStat.Vitality.RemoveAllModifiersFromSource(this);

    }
}
