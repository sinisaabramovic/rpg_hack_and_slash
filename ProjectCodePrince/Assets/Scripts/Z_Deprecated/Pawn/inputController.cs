using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputController : MonoBehaviour {

	// Use this for initialization
    public enum PathDirection
    {
        left,
        right,
        up,
        down,
        idle,
        begin
    }

	public PathDirection pathDirection;
    public float swipeValue;
    private Vector3 mouseDownPos;


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPos = Input.mousePosition;
            pathDirection = PathDirection.begin;
        }

        if (Input.GetMouseButtonUp(0))
        {
            pathDirection = PathDirection.idle;

            if (Input.mousePosition.y > mouseDownPos.y)
            {
                Debug.Log("Drag up!");
                pathDirection = PathDirection.up;
            }
            else if (Input.mousePosition.y < mouseDownPos.y)
            {
                Debug.Log("Drag down!");
                pathDirection = PathDirection.down;
                swipeValue = Mathf.Sign(Input.mousePosition.y - mouseDownPos.y);
            }

            if (Input.mousePosition.x > mouseDownPos.x)
            {
                Debug.Log("Drag right!");
                pathDirection = PathDirection.right;
            }
            else if (Input.mousePosition.x < mouseDownPos.x)
            {
                Debug.Log("Drag left!");
                pathDirection = PathDirection.left;
                swipeValue = Mathf.Sign(Input.mousePosition.x - mouseDownPos.x);

            }
        }
	}
}
