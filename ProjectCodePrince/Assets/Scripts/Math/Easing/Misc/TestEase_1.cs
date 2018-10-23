using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEase_1 : MonoBehaviour {

    Easer easer;
	// Use this for initialization
	void Start () {
        //easer = new PowerEase(transform.position.x, transform.position.x + 15, 3, 5, Easer.EaseType.OUT);
        easer = new PowerEase(transform.localScale.x, transform.localScale.x + 15, 3, 1, Easer.EaseType.IN);

        easer.RunEase(this, 2);
	}
	
	// Update is called once per frame
	void Update () {
        if(easer.isEase()){
            Vector3 newPos = new Vector3(easer.GetValue(),transform.localScale.y, transform.localScale.z);
            //transform.position = newPos;
            transform.localScale = newPos;
        }
	}
}
