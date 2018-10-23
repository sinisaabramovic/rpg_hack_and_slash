using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTornadoMove : MonoBehaviour {

	// Use this for initialization


    public LayerMask enemyLayer;
    public float radius = 0.5f;
    public float damageCount = 10f;
    public GameObject fireExplosion;

    private EnemyHealth enemyHealth;
    private bool collider;
    private float speed = 0.2f;
    // Update is called once per frame

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        transform.rotation = Quaternion.LookRotation(player.transform.forward);
    }

    void Update () {
        Move();
        CheckForDamage();
	}

    void Move(){
        transform.Translate(Vector3.forward * (speed + Time.deltaTime));
    }

    void CheckForDamage(){
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemyLayer);

        foreach (Collider c in hits)
        {
            if (c.isTrigger)
            {
                continue;
            }

            enemyHealth = c.GetComponent<EnemyHealth>();
            collider = true;

            if (collider)
            {
                enemyHealth.TakeDamge(damageCount);
                Vector3 temp = transform.position;
                temp.y = 2f;
                Instantiate(fireExplosion, temp, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
