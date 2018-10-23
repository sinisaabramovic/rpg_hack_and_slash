using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour {
    private int diceRollResult = 0;
    private Dice[] dices;
    private int MAX_NUMBER_OF_DICE = 10;
	// Use this for initialization
	void Awake () {
        Init();
	}
	
	// Update is called once per frame

    private void ResetDiceResult(){
        diceRollResult = 0;
    }

    public void Init(){
        dices = new Dice[MAX_NUMBER_OF_DICE];

        for (int i = 0; i < MAX_NUMBER_OF_DICE; i++)
        {
            dices[i] = new Dice();
        }
    }

    public int RollDices(int diceNum){

        ResetDiceResult();

        for (int i = 0; i < diceNum; i++){
            int rollValue = dices[i].Roll();
            //Debug.Log("ROLL RESULT FOR TURN " + i + " is " + rollValue);
            diceRollResult += rollValue;
        }

        //Debug.Log("TOTAL ROLL RESULT FOR " + diceNum + " is " + diceRollResult);

        return diceRollResult;
    }

	void Update () {
        //int k = dices[1].Roll();
	}
}
