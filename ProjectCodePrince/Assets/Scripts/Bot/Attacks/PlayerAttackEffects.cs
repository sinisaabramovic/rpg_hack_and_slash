﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sino.CharacterStats;

public class PlayerAttackEffects : MonoBehaviour {

    // Use this for initialization

    public Sino.CharacterStats.PlayerCharacter character;

    public void Awake()
    {
        character = GetComponent<Sino.CharacterStats.PlayerCharacter>();
    }

    public GameObject groundImpact_Spawn, kickFX_Spawn, fireTornado_Spawn, fireShield_Spawn;

    public GameObject groundImpact_Prefab, kickFX_Prefab, fireTornado_Prefab, fireShield_Prefab, healFX_Prefab, thunderFX_Prefab;

    void GroundImpact(){
        Instantiate(groundImpact_Prefab, groundImpact_Spawn.transform.position, Quaternion.identity);
    }

    void Kick()
    {
        GameObject hiter = Instantiate(kickFX_Prefab, kickFX_Spawn.transform.position, Quaternion.identity);
        hiter.GetComponent<SkillDamage>().radius = character.Weapon.GetComponent<Weapon>().Radius;
    }

    void FireTornado()
    {
        Instantiate(fireTornado_Prefab, fireTornado_Spawn.transform.position, Quaternion.identity);
    }

    void FireShield()
    {
        GameObject fireObj = Instantiate(fireShield_Prefab, fireShield_Spawn.transform.position, Quaternion.identity) as GameObject;

        fireObj.transform.SetParent(transform);
    }

    void Heal()
    {
        Vector3 temp = transform.position;
        temp.y += 2f;

        GameObject healObj = Instantiate(healFX_Prefab, temp, Quaternion.identity) as GameObject;
        healObj.transform.SetParent(transform);
    }

    void ThunderAttack()
    {
        for (int i = 0; i < 8; i++){
            Vector3 pos = Vector3.zero;


            if(i==0){
                pos = new Vector3(transform.position.x - 4f, transform.position.y + 2f, transform.position.z);
            }else if(i == 1){
                pos = new Vector3(transform.position.x + 4f, transform.position.y + 2f, transform.position.z);
            }else if (i == 2)
            {
                pos = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z - 4f);
            }else if (i == 3)
            {
                pos = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z + 4f);
            }else if (i == 4)
            {
                pos = new Vector3(transform.position.x + 2.5f, transform.position.y + 2f, transform.position.z + 2.5f);
            }else if (i == 5)
            {
                pos = new Vector3(transform.position.x - 2.5f, transform.position.y + 2f, transform.position.z + 2.5f);
            }else if (i == 6)
            {
                pos = new Vector3(transform.position.x - 2.5f, transform.position.y + 2f, transform.position.z - 2.5f);
            }else if (i == 7)
            {
                pos = new Vector3(transform.position.x + 2.5f, transform.position.y + 2f, transform.position.z + 2.5f);
            }

            Instantiate(thunderFX_Prefab, pos, Quaternion.identity);
        }
    }
}
