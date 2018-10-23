using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    // Use this for initialization

    public float health = 100f;

    public void TakeDamge(float amount){
        health -= amount;

        //print("Enemy took damge, health is " + health);
        if(health <= 0){
            
        }
    }
}
