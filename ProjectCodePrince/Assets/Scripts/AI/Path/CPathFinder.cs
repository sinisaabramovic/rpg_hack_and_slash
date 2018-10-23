using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class CPathFinder : MonoBehaviour {
    
    CGrid grid;

	private void Awake()
	{
        grid = GetComponent<CGrid>();
	}

    public void FindPath(PathRequest request, Action<PathResult> callback){

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        CNode startNode = grid.GetNodeFromWorldPoint(request.pathStart);
        CNode endNode = grid.GetNodeFromWorldPoint(request.pathEnd);

        if(startNode.walkable && endNode.walkable){
            CHeap<CNode> openSet = new CHeap<CNode>(grid.MaxSize);
            HashSet<CNode> closedSet = new HashSet<CNode>();

            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                CNode currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == endNode)
                {
                    stopwatch.Stop();
                    print("Path found: " + stopwatch.ElapsedMilliseconds + " ms");
                    pathSuccess = true;
                    break;
                }

                foreach (CNode neighbour in grid.GetNeighbours(currentNode))
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour) + neighbour.movementPenalty;
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, endNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }else{
                            openSet.UpdateItem(neighbour);
                        }
                    }
                }
            }
        }
              
        if(pathSuccess){
            waypoints = RetracePath(startNode, endNode);
            pathSuccess = waypoints.Length > 0;
        }

        callback(new PathResult(waypoints, pathSuccess, request.callback));
    }

    Vector3[] RetracePath(CNode startNode, CNode endNode){
        List<CNode> path = new List<CNode>();
        CNode currentNode = endNode;

        while(currentNode != startNode){
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    Vector3[] SimplifyPath(List<CNode> path){
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++){
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
                if(directionNew != directionOld){
                    waypoints.Add(path[i].worldPosition);
                }
            directionOld = directionNew;
        }

        return waypoints.ToArray();
    }

    int GetDistance(CNode nodeA, CNode nodeB){

        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if(dstX > dstY){
            return 14 * dstY + 10 * (dstX - dstY);
        }else{
            return 14 * dstX + 10 * (dstY - dstX);
        }

        //return 0;
    }
}
