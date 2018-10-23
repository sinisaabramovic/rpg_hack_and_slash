using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    // Use this for initialization
    public float damage = 10f;

    public LayerMask playerLayer;
    public float radius = 0.5f;
    public float damageCount = 10f;
    public GameObject fxAttack;

    private PlayerHealth playerHealth;
    private bool collided;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, playerLayer);

        foreach (Collider c in hits)
        {
            if (c.isTrigger)
            {
                continue;
            }

            playerHealth = c.GetComponent<PlayerHealth>();
            collided = true;

            if (collided)
            {
                playerHealth.TakeDamage(damageCount);
                Instantiate(fxAttack, transform.position, Quaternion.identity);
                enabled = false;
            }
        }
	}
}
