using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealScript : MonoBehaviour {

    // Use this for initialization
    public float healAmount = 10f;
	void Start () {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().health += healAmount;
        print("Player health is : " + GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().health);
	}
	
}
