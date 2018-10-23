using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerEase : Easer {

    private float power;

    public PowerEase(float _from, float _to, float _power, float _duration, EaseType _type) : base(_from, _to, _duration, _type){
        this.power = _power;
    }

    protected override float EasingInEquation(float _currentPosition){
        return from + (to - from) * Mathf.Pow(_currentPosition, 1.0f / power);
    }
    protected override float EasingOutEquation(float _currentPosition){
        return from + (to - from) * Mathf.Pow(_currentPosition, power);
    }
}
