using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;

namespace Sino.CharacterStats
{
    [Serializable]
    public class CharacterStats
    {

        public float BaseValue;

        protected readonly List<StatsModifier> statsModifiers;
        public readonly ReadOnlyCollection<StatsModifier> StatsModifiers;

        public virtual float Value
        {
            get
            {
                if (isChanged || BaseValue != lastBaseValue)
                {
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


        public CharacterStats(float baseValue) : this()
        {
            BaseValue = baseValue;
        }

        public CharacterStats()
        {
            statsModifiers = new List<StatsModifier>();
            StatsModifiers = statsModifiers.AsReadOnly();
        }

        public virtual void AddModifier(StatsModifier mod)
        {
            isChanged = true;
            statsModifiers.Add(mod);
            statsModifiers.Sort(CompareModifuerOrder);
        }

        protected virtual int CompareModifuerOrder(StatsModifier a, StatsModifier b)
        {
            if (a.Order < b.Order)
            {
                return -1;
            }
            else if (a.Order > b.Order)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public virtual bool RemoveModifier(StatsModifier mod)
        {

            if (statsModifiers.Remove(mod))
            {
                isChanged = true;
                return true;
            }

            return false;
        }

        public virtual bool RemoveAllModifiersFromSource(object source)
        {

            bool didRemove = false;

            for (int i = statsModifiers.Count - 1; i >= 0; i--)
            {
                if (statsModifiers[i].Source == source)
                {
                    isChanged = true;
                    didRemove = true;
                    statsModifiers.RemoveAt(i);
                }
            }

            return didRemove;
        }

        protected virtual float CalculateFinalValue()
        {
            float finalValeu = BaseValue;
            float sumPercentAdd = 0;


            for (int i = 0; i < statsModifiers.Count; i++)
            {
                StatsModifier mod = statsModifiers[i];
                if (mod.statModType == StatModType.Flat)
                {
                    finalValeu += mod.Value;
                }
                else if (mod.statModType == StatModType.PercentAdd)
                {
                    sumPercentAdd += mod.Value;
                    if (i + 1 >= statsModifiers.Count || statsModifiers[i + 1].statModType != StatModType.PercentAdd)
                    {
                        finalValeu *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                }
                else if (mod.statModType == StatModType.PercentMult)
                {
                    finalValeu *= 1 + mod.Value;
                }
            }

            return (float)Math.Round(finalValeu, 4);
        }
    }
}

