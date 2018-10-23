using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour {

	// Use this for initialization
	void Start () {
        if(isLocalPlayer){
            GetComponent<PlayerMoveAgent>().enabled = true;
            IzoCamera.Player = this.gameObject;
        }else{
            GetComponent<PlayerMoveAgent>().enabled = false;
        }
	}
	
}
