using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;

[Serializable]
public class WeaponStats {

    public float BaseValue;

    protected readonly List<WeaponStatsModifier> statsModifiers;
    public readonly ReadOnlyCollection<WeaponStatsModifier> StatsModifiers;

    public virtual float Value{ 
        get { 
            if(isChanged || BaseValue != lastBaseValue){
                lastBaseValue = BaseValue;
                _value = CalculateFinalValue();
                isChanged = false;
            }
            return _value; 
        }
    }

    protected bool isChanged = true;
    protected float _value;
    protected float lastBaseValue = float.MinValue;


    public WeaponStats(float baseValue) : this(){
        BaseValue = baseValue;
    }

    public WeaponStats()
    {
        statsModifiers = new List<WeaponStatsModifier>();
        StatsModifiers = statsModifiers.AsReadOnly();
    }

    public virtual void AddModifier(WeaponStatsModifier mod){
        isChanged = true;
        statsModifiers.Add(mod);
        statsModifiers.Sort(CompareModifuerOrder);
    }

    protected virtual int CompareModifuerOrder(WeaponStatsModifier a, WeaponStatsModifier b){
        if(a.Order < b.Order){
            return -1;
        }else if(a.Order > b.Order){
            return 1;
        }else{
            return 0;
        }
    }

    public virtual bool RemoveModifier(WeaponStatsModifier mod){
        
        if(statsModifiers.Remove(mod)) {
            isChanged = true;
            return true;
        }

        return false;
    }

    public virtual bool RemoveAllModifiersFromSource(object source){

        bool didRemove = false;

        for (int i = statsModifiers.Count - 1; i >= 0; i--){
            if(statsModifiers[i].Source == source){
                isChanged = true;
                didRemove = true;
                statsModifiers.RemoveAt(i);
            }
        }

        return didRemove;
    }

    protected virtual float CalculateFinalValue(){
        float finalValeu = BaseValue;
        float sumPercentAdd = 0;


        for (int i = 0; i < statsModifiers.Count; i++)
        {
            
            WeaponStatsModifier mod = statsModifiers[i];
            if (mod.statModType == WeaponStatModType.Flat)
            {
                finalValeu += mod.Value;
            }else if(mod.statModType == WeaponStatModType.PercentAdd){
                sumPercentAdd += mod.Value;
                if(i + 1 >= statsModifiers.Count || statsModifiers[i + 1].statModType != WeaponStatModType.PercentAdd){
                    finalValeu *= 1 + sumPercentAdd;
                    sumPercentAdd = 0;
                }
            }
            else if (mod.statModType == WeaponStatModType.PercentMult)
            {
                finalValeu *= 1 + mod.Value;
            }
        }

        return (float)Math.Round(finalValeu, 4);
    }
}
