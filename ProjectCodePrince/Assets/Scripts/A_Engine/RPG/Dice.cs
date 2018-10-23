using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice{

    private int die;

    public const int maxRollDieValue = 7;
    public int Die
    {
        get { return die; }
        set { die = value; }
    }

    public int Roll()
    {
        //get a random number object we can the use to determine the die face
        die = Random.Range(1, maxRollDieValue);

        // To prevent get maxRollValue
        if(die >= maxRollDieValue){
            die = maxRollDieValue - 1;
        }

        return die;
    }
}
