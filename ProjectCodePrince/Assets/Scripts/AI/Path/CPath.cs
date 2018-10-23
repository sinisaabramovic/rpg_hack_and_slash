using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPath {

    public readonly Vector3[] lookPoints;
    public readonly CLine[] turnBoundaries;
    public readonly int finishLineIndex;
    public readonly int slowDownIndex;

    public CPath(Vector3[] waypoints, Vector3 startPos, float turnDist, float stoppingDistance){
        lookPoints = waypoints;
        turnBoundaries = new CLine[lookPoints.Length];
        finishLineIndex = turnBoundaries.Length -1;

        Vector2 previousPoint = V3ToV2(startPos);
        for (int i = 0; i < lookPoints.Length; i++){
            Vector2 currentPoint = V3ToV2(lookPoints[i]);
            Vector2 dirToCurrentPoint = (currentPoint - previousPoint).normalized;
            Vector2 turnBoundatyPoint = (i== finishLineIndex)?currentPoint : currentPoint - dirToCurrentPoint * turnDist;
            turnBoundaries[i] = new CLine(turnBoundatyPoint, previousPoint - dirToCurrentPoint * turnDist);
            previousPoint = turnBoundatyPoint;
        }

        float distanceFromEndPoint = 0;
        for (int i = lookPoints.Length - 1; i > 0; i--){
            distanceFromEndPoint += Vector3.Distance(lookPoints[i], lookPoints[i - 1]);
            if(distanceFromEndPoint > stoppingDistance){
                slowDownIndex = i;
                break;
            }
        }

    }

    Vector2 V3ToV2(Vector3 v3){
        return new Vector2(v3.x, v3.z);
    }

    public void DrawWithGizmos(){
        Gizmos.color = Color.black;
        foreach(Vector3 p in lookPoints){
            Gizmos.DrawCube(p + Vector3.up, Vector3.one);
        }

        Gizmos.color = Color.white;
        foreach(CLine l in turnBoundaries){
            l.DrawWithGizmos(10);
        }
    }
}
