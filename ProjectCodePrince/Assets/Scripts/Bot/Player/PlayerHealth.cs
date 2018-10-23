using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sino.CharacterStats;

public class PlayerHealth : MonoBehaviour {

    // Use this for initialization

    public float health = 100f;
    public bool isDeath = false;

    private bool isShield = false;
    private Sino.CharacterStats.PlayerCharacter character;
	void Start () {
        character = GetComponent<Sino.CharacterStats.PlayerCharacter>();
	}
	
	// Update is called once per frame
	void Update () {
        if(character.Shield != null){
            isShield = true;
        }
	}

    public bool Shielded{
        get{ return isShield; }
        set{ isShield = value; }
    }

    private float calculateDamage(float amount){
        if(character.Shield != null){
            float damage_taken = amount * ((100 - character.CharacterBlockDamage)/100);
            damage_taken = Mathf.Abs(damage_taken);
            return damage_taken;

        }else{
            return amount;
        }
    }

    public void TakeDamage(float amount){
        
        health -= calculateDamage(amount);
        if (health <= 0)
        {
            isDeath = true;
        }
    }
}
