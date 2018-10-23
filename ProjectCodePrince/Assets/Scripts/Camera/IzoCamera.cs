﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IzoCamera : MonoBehaviour {

    static public GameObject Player;
    public float OffsetX = -35;
    public float OffsetZ = -40;
    public float OffsetY = 30;
    public float MaximumDistance = 2;
    public float PlayerVelocity = 10;
    private float _movmentX;
    private float _movmentY;
    private float _movmentZ;
    // Use this for initialization
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Player != null){
            _movmentX = ((Player.transform.position.x + OffsetX - this.transform.position.x)) / MaximumDistance;
            _movmentY = ((Player.transform.position.y + OffsetY - this.transform.position.y)) / MaximumDistance;
            _movmentZ = ((Player.transform.position.z + OffsetZ - this.transform.position.z)) / MaximumDistance;
            this.transform.position += new Vector3((_movmentX * PlayerVelocity * Time.deltaTime), (_movmentY * PlayerVelocity * Time.deltaTime), (_movmentZ * PlayerVelocity * Time.deltaTime));          
        }        
    }
}
