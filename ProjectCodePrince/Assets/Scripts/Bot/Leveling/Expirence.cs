using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expirence : MonoBehaviour {

	// Use this for initialization
    public float calcExpirence(int monsterLevel,int playerLevel, float base_Amount, int number_of_players){
        float expirence_return = 0f;
        expirence_return = (base_Amount / number_of_players) * (1.0f + 0.1f * (monsterLevel - playerLevel));
        expirence_return = Mathf.Round(expirence_return);
        if(expirence_return < 0f){
            expirence_return = 0f;
        }
        return expirence_return;
    }

}
