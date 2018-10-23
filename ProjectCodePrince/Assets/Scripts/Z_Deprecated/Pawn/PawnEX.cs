using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnEX : MonoBehaviour {

    // Use this for initialization
    public makeSpline path;
	public float smoothTime = 0.3F;

    private CharacterController pawnController;
    private int currentWaypoint = 0;
    private float speed = 8.0f;
    private Vector3 velocity = Vector3.zero;
    private inputController inputControll;
    private float maxDistanceToJump = 2.0f;
    public float jumpSpeed = 22.0F;
    public float gravity = 20.0F;
    Vector3 moveDirection;

	void Start () {

        pawnController = GetComponent<CharacterController>();
        inputControll = GetComponent<inputController>();
        moveDirection = Vector3.zero;
		
	}
	
	// Update is called once per frame
	void Update () {		
        if(path.pathPositions.Count > 0){

            if(currentWaypoint < path.pathPositions.Count){
                Vector3 target = path.pathPositions[currentWaypoint];

                if(inputControll.pathDirection == inputController.PathDirection.right){
                    if(transform.position.x > target.x){
                        currentWaypoint++;
                        return;
                    }
                }

                if (inputControll.pathDirection == inputController.PathDirection.left)
                {
                    if (transform.position.x < target.x)
                    {
                        currentWaypoint++;
                        return;
                    }
                }

                //target.y = transform.position.y; 
                moveDirection = target - transform.position;
                float distance = Vector3.Distance(target, transform.position);

                float distanceCalc = (speed * 2) / 10; 

                if(currentWaypoint == path.pathPositions.Count){
                    distanceCalc = 1f;
                }

                if (distance < distanceCalc)
                {
                    //transform.position = target; 
                    currentWaypoint++;
                }
                else
                {
                    //transform.LookAt(target);

                    //if (target.y - transform.position.y)
                    //Debug.Log(transform.position.y - target.y);
                    bool isJumping = false;
                    if(transform.position.y - target.y < 0 && pawnController.isGrounded){
                        moveDirection.y = jumpSpeed;
                        isJumping = true;
                    }
                    //Debug.Log(pawnController.isGrounded);

 

                    //pawnController.Move(moveDirection * speed * Time.deltaTime);
                    //if(isJumping){
                    //    pawnController.Move(moveDirection * speed * Time.deltaTime);    
                    //}else{
                    //    pawnController.Move(moveDirection.normalized * speed * Time.deltaTime);    
                    //}

                    //transform.Translate(moveDirection.normalized * Time.deltaTime * speed, Space.World);
                    //float smoothFactor = 2.0f;

                    //transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 2.0f);

                    //transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
                }
            }
        }else{
            currentWaypoint = 0;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        pawnController.Move(moveDirection.normalized * speed * Time.deltaTime);  
	}
}
