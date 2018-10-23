﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour {

    const float minPathUpdateTime = 0.2f;
    const float pathUpdateMoveThreshold = 0.5f;

    public Transform target;
    public float speed = 40.0f;
    public float turnDist = 5.0f;
    public float turnSpeed = 5.0f;
    public float stoppingDistance = 10.0f;

    CPath path;

	private void Start()
	{
        StartCoroutine(UpdatePath());
	}

    public void OnPahFound(Vector3[] waypoints, bool pathSuccessful){
        if(pathSuccessful){
            path = new CPath(waypoints, transform.position, turnDist, stoppingDistance);
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator UpdatePath(){

        if(Time.timeSinceLevelLoad < 0.3f || target == null){
            yield return new WaitForSeconds(0.3f);
        }
        CPathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPahFound));

        float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
        Vector3 targetPosOld = target.position;

        while(true){
            yield return new WaitForSeconds(minPathUpdateTime);
            if((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold){
                CPathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPahFound));
                targetPosOld = target.position;
            }
           
        }
    }

    IEnumerator FollowPath(){

        bool followingPath = true;
        int pathIndex = 0;

        transform.LookAt(path.lookPoints[0]);

        float speedPercent = 1.0f;

        while(followingPath){
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
            while(path.turnBoundaries[pathIndex].HasCrossedLine(pos2D)){
                if(pathIndex == path.finishLineIndex){
                    followingPath = false;
                    break;
                }else{
                    pathIndex++;
                }
            }

            if(followingPath){

                if(pathIndex >= path.slowDownIndex && stoppingDistance > 0){
                    speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(pos2D) / stoppingDistance);
                    if(speedPercent < 0.01f){
                        followingPath = false;
                    }
                }



                Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
                transform.Translate(Vector3.forward * Time.deltaTime * speed * speedPercent, Space.Self);
            }
            yield return null;
        }
    }

	public void OnDrawGizmos()
	{
        if(path != null){
            path.DrawWithGizmos();
        }
	}

}
