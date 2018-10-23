using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    // Use this for initialization
    public GameObject[] players;
    public bool playersFounded = false;
    private float currentSearchTime = 0f;
    private float waitSearchTime = 2f;

    private bool accessRequested = false;

    public bool RequestAccessed{
        get { return accessRequested; }
        set { accessRequested = value; }
    }

    void SearchForPlayers()
    {

        if (currentSearchTime >= waitSearchTime && !accessRequested)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            if (players != null)
            {
                if(players.Length > 0){
                    playersFounded = true;
                }else{
                    playersFounded = false;
                }
            }
            else
            {
                playersFounded = false;
            }
        }
        else
        {
            currentSearchTime += Time.deltaTime;
        }
    }

	void Awake () {
        players = GameObject.FindGameObjectsWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        SearchForPlayers();
	}
}
