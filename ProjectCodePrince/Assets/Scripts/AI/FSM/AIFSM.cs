using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using States;

public class AIFSM : MonoBehaviour
{

    // Use this for initialization
    public bool switchState = false;
    public float gameTimer = 0.0f;
    public int seconds = 0;

    public StateMachine<AIFSM> stateMachine {get; set;}

	private void Start()
	{
        stateMachine = new StateMachine<AIFSM>(this);
        stateMachine.ChangeState(FirstState.Instance);
        gameTimer = Time.time;
	}

	private void Update()
	{
        if(Time.time > gameTimer + 1){
            gameTimer = Time.time;
            seconds++;
            Debug.Log("PASSS " + seconds);

        }

        if(seconds == 5){
            seconds = 0;
            switchState = !switchState;
            stateMachine.Update();
        }


	}

}
