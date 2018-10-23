using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Easer {

    // Enum
    public enum EaseType
    {
        IN,
        OUT
    };

    protected float from, to, duration, elapsedTime, delay;
    protected Coroutine rutine;
    protected MonoBehaviour m;
    protected EaseType type;

    protected bool isEasing;
    protected float value;

    public Easer(float _from, float _to, float _duraton, EaseType _type){
        this.from = _from;
        this.to = _to;
        this.duration = _duraton;
        this.type = _type;

        isEasing = false;
        value = _from;
        elapsedTime = 0;
    }

    public void PauseEase(){
        if(m != null){
            m.StopCoroutine(rutine);
            isEasing = false;
        }
    }

    public void StopEase(){
        PauseEase();
        value = this.from;
        elapsedTime = 0;
    }

    public bool isEase(){
        return isEasing;
    }

    public float GetValue(){
        return value;
    }

    // Starts the Ease and moves object
    public void RunEase(MonoBehaviour _m, float _delay = 0){
        if(!isEasing){
            isEasing = true;
            this.m = _m;
            this.delay = _delay;
            rutine = m.StartCoroutine(MoveObject());
        }
    }

    protected IEnumerator MoveObject(){
        yield return new WaitForSeconds(delay);
        float currentPosition;
        while(elapsedTime < duration){
            currentPosition = elapsedTime / duration;
            value = (type == EaseType.IN) ? EasingInEquation(currentPosition) : EasingOutEquation(currentPosition);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        value = to;
        yield return new WaitForEndOfFrame();
        isEasing = false;
    }

    // Calculate of value of the ease in function and ou function
    protected abstract float EasingInEquation(float _currentPosition);
    protected abstract float EasingOutEquation(float _currentPosition);
}
