using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    // Use this for initialization

    public bool testSpawnPlayer = false;
    private bool testPrivatePlayer = false;

    private int playerNum = 0;
    private const int MAX_PLAYER_NUM = 4;

    public Transform[] playersSpawns;
    public GameObject[] playersBots;

    public Transform player_1_spawn_position;
    public Transform player_2_spawn_position;
    public Transform player_3_spawn_position;
    public Transform player_4_spawn_position;

    public GameObject viking;

    void Awake()
    {
        playersBots = new GameObject[4];
        playersSpawns = new Transform[4];
        playersSpawns[0] = player_1_spawn_position;
        playersSpawns[1] = player_2_spawn_position;
        playersSpawns[2] = player_3_spawn_position;
        playersSpawns[3] = player_4_spawn_position;


    }
	
    void spawnPlayer(int playerIndex){
        Instantiate(viking, playersSpawns[playerIndex].position, Quaternion.identity);
    }

    void removePlayer(int playerIndex){
        
    }

	// Update is called once per frame
	void Update () {
        if(testSpawnPlayer && !testPrivatePlayer){
            spawnPlayer(0);
            testPrivatePlayer = true;
        }
	}
}
