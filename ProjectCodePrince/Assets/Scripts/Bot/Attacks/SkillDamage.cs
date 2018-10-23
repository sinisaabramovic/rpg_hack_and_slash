using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamage : MonoBehaviour {

    // Use this for initialization

    public LayerMask enemyLayer;
    public float radius = 0.5f;
    public float damageCount = 10f;

    private EnemyHealth enemyHealth;
    private bool collided;

    // Update is called once per frame
	void Update () {

        Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemyLayer);
		
        foreach(Collider c in hits){
            if(c.isTrigger){
                continue;
            }

            enemyHealth = c.GetComponent<EnemyHealth>();
            collided = true;

            if(collided){
                enemyHealth.TakeDamge(damageCount);
                enabled = false;
            }
        }
	}
}
