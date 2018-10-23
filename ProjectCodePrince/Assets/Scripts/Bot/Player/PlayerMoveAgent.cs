using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Sino.CharacterStats;

public class PlayerMoveAgent : MonoBehaviour
{

    // Use this for initialization

    private Animator animator;  
    private NavMeshAgent navMeshAgent;

    private float attack_Distance = 0.5f;


    private bool enemySpot = false;
    public LayerMask terrainMask;

    private float distance = 0;
    private bool canMove = true;
    private Vector3 targetPos;
    private PlayerAttack playerAttack;
    private Sino.CharacterStats.PlayerCharacter Character;

    void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerAttack = GetComponent<PlayerAttack>();
        Character = GetComponent<Sino.CharacterStats.PlayerCharacter>();
        targetPos = transform.position;
    }

    public float AttackDistance{
        get { return attack_Distance; }
        set { attack_Distance = value; }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, terrainMask))
            { 
                if(hit.transform.gameObject.tag == "ground"){
                    targetPos = hit.point;
                    attack_Distance = 0.5f;
                    enemySpot = false;
                    navMeshAgent.SetDestination(targetPos);
                }
                    
                if (hit.transform.gameObject.tag == "Enemy"){
                    targetPos = hit.point;
                    attack_Distance = 3.5f;
                    enemySpot = true;
                    navMeshAgent.SetDestination(targetPos);
                }
                    
            }
        }       

        distance = Vector3.Distance(transform.position, targetPos);
       
        if (distance > attack_Distance && !playerAttack.CanAttack)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = Character.CharacterMoveSpeed;
            animator.SetBool("Run", true);
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(targetPos - transform.position),
                                                  15f * Time.deltaTime);
            if (enemySpot)
            {
                navMeshAgent.SetDestination(targetPos);
            }
        }
        else
        {
            navMeshAgent.isStopped = true;
            animator.SetBool("Run", false);

            if (enemySpot){
                enemySpot = false;
                //playerAttack.PlayerAttacks();
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      Quaternion.LookRotation(targetPos - transform.position),
                                                      15f * Time.deltaTime);
            }
        }
    }
}

