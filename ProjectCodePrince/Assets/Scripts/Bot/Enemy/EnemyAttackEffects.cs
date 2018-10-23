using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackEffects : MonoBehaviour {

	// Use this for initialization
    public GameObject attack_spawn;

    public GameObject attack_prefab;

    void SwordBasic(float damage)
    {
        attack_prefab.GetComponent<EnemyAttack>().damage = damage;
        Instantiate(attack_prefab, attack_spawn.transform.position, Quaternion.identity);
    }
}
