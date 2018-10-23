using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShield : MonoBehaviour {

    // Use this for initialization

    private PlayerHealth playerHealth;
	void Awake () {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
	}

    private void OnEnable()
    {
        playerHealth.Shielded = true;
    }

    private void OnDisable()
    {
        playerHealth.Shielded = false;
    }
    // Update is called once per frame
    void Update () {
        
	}

}
