using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimationCallBack : MonoBehaviour {

    // Use this for initialization
    private PlayerAttack playerAttack;

	void Start () {
        playerAttack = GetComponent<PlayerAttack>();
	}
	
	// Update is called once per frame
	void setAnimationEndCallBack () {
        print("ENEMY CAN BE ATTACKED");
        playerAttack.CanAttack = false;
	}
}
