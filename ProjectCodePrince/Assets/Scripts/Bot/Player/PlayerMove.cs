using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    // Use this for initialization

    private Animator anims;
    private CharacterController characterController;
    private CollisionFlags collisionFlags = CollisionFlags.None;

    private float moveSpeed = 6.0f;
    private bool canMove;
    private bool finishedMovement = true;

    private Vector3 targetPos = Vector3.zero;
    private Vector3 playerMove = Vector3.zero;

    private float playerToPointDistance;
    private float gravity = 9.8f;
    private float height;

    public GameObject debugPointer;

    public LayerMask terrainMask;


    void Awake()
    {
        anims = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateHeight();
        ChekIfFinishedMvement();
    }

    bool IsGrounded()
    {
        return collisionFlags == CollisionFlags.CollidedBelow ? true : false;
    }

    void ChekIfFinishedMvement()
    {
        if (!finishedMovement)
        {
            if (!anims.IsInTransition(0) && !anims.GetCurrentAnimatorStateInfo(0).IsName("Stand") && anims.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                finishedMovement = true;
            }
        }
        else
        {
            MovePlayer();
            playerMove.y = height * Time.deltaTime;
            collisionFlags = characterController.Move(playerMove);
        }
    }

    void CalculateHeight()
    {
        if (IsGrounded())
        {
            height = 0f;
        }
        else
        {
            height -= gravity * Time.deltaTime;
        }
    }

    void MovePlayer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, terrainMask))
            {
                playerToPointDistance = Vector3.Distance(transform.position, hit.point);

                if (playerToPointDistance >= 1.0f)
                {
                    canMove = true;
                    targetPos = hit.point;
                    //debugPointer.transform.position = targetPos;
                }
            }

        }

        if (canMove)
        {
            anims.SetFloat("Walk", playerToPointDistance);

            Vector3 targetTemp = new Vector3(targetPos.x, transform.position.y, targetPos.z);

            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(targetTemp - transform.position),
                                                  15.0f * Time.deltaTime);

            playerMove = transform.forward * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, targetPos) <= 0.5f)
            {
                canMove = false;
            }

        }
        else
        {
            playerMove.Set(0f, 0f, 0f);
            anims.SetFloat("Walk", 0f);
        }

    }

    public bool FinishedMovement
    {
        get
        {
            return finishedMovement;
        }
        set
        {
            finishedMovement = value;
        }
    }

    public Vector3 TargetPosition
    {
        get
        {
            return targetPos;
        }
        set
        {
            targetPos = value;
        }
    }

}

