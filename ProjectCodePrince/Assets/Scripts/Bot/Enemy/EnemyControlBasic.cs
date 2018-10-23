using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControlBasic : MonoBehaviour {

    // Use this for initialization

    public Transform[] walkPoints;
    private EnemyManager enemyManager;
    private int walk_Index = 0;

    private Transform playerTarget;
    private bool playerSpooted = false;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private float walk_Speed = 1.6f;
    private float run_Speed = 3.4f;

    private float walk_Distance = 8f;
    private float attack_Distance = 3.0f;

    private float currentAttackTime = 0f;
    private float waitAttackTime = 1f;

    private float currentSearchTime = 0f;
    private float waitSearchTime = 2f;

    private Vector3 nextDestination;

	void Awake () {

        enemyManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemyManager>();

        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
	}

    void SearchForPlayer(){
        
        if (currentSearchTime >= waitSearchTime)
        {
            enemyManager.RequestAccessed = true;
            if (enemyManager.playersFounded)
            {
                foreach (GameObject player in enemyManager.players)
                {
                    playerTarget = player.transform;
                    playerSpooted = true;
                }
            }else{
                playerSpooted = false;
            }
            enemyManager.RequestAccessed = false;
        }
        else
        {
            enemyManager.RequestAccessed = false;
            currentSearchTime += Time.deltaTime;
        }
        
    }
	
    // Just in case if player target become nil
    Vector3 getPlayerTargetPosition(){
        if(playerTarget){
            return playerTarget.position;
        }else{
            return transform.position;
        }
    }

	// Update is called once per frame
	void Update () {

        SearchForPlayer();

        float distance = 0f;
        if(playerSpooted){
            distance = Vector3.Distance(transform.position, getPlayerTargetPosition());
        }else{
            distance = 1000f;
        }

        if(distance > walk_Distance){            
            if(navMeshAgent.remainingDistance <= 0.5f){
                //navMeshAgent.SetDestination(new Vector3(1f, 1f, 1f));
                navMeshAgent.isStopped = false;

                animator.SetBool("Walk", true);
                animator.SetBool("Run", false);
                navMeshAgent.speed = walk_Speed;
                animator.SetInteger("Atk", 0);

                nextDestination = walkPoints[walk_Index].position;
                navMeshAgent.SetDestination(nextDestination);

                if(walk_Index == walkPoints.Length -1){
                    walk_Index = 0;
                }else{
                    walk_Index++;
                }
            }
        }else{
            if(distance > attack_Distance){
                navMeshAgent.isStopped = false;
                navMeshAgent.speed = run_Speed;
                animator.SetBool("Walk", false);
                animator.SetBool("Run", true);
                animator.SetInteger("Atk", 0);
                navMeshAgent.SetDestination(getPlayerTargetPosition());
            }else{
                
                navMeshAgent.isStopped = true;
                animator.SetBool("Run", false);
                Vector3 localTargetPosition = getPlayerTargetPosition();
                Vector3 targetPosition = new Vector3(localTargetPosition.x,
                                                     transform.position.y,
                                                     localTargetPosition.z);
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      Quaternion.LookRotation(targetPosition - transform.position),
                                                      5f * Time.deltaTime);
                if(currentAttackTime >= waitAttackTime){
                    int atkRange = Random.Range(1, 3);
                    animator.SetInteger("Atk", atkRange);
                    currentAttackTime = 0f;
                    //EnemyAttack atk = GetComponent<EnemyAttack>();
                }else{
                    animator.SetInteger("Atk", 0);
                    currentAttackTime += Time.deltaTime;
                }
            }
        }
	}
}
