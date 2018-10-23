using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {

    // Use this for initialization
    private Transform target;
    private Vector3 cameraTarget;

    [SerializeField]
    //private float followSpeed = 8f;
    //private float xOffset = 8f;

    public float smoothTime = 0.6f;
    private Vector3 velocity = Vector3.zero;

    void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {

       // cameraTarget = new Vector3(target.position.x, transform.position.y, target.position.z);
       // transform.position = Vector3.Lerp(transform.position, cameraTarget, Time.deltaTime * followSpeed);

        Vector3 goalPos = target.position;
        goalPos.y = transform.position.y;
        goalPos.x -= 15f;
        goalPos.z -= 15f;
        transform.position = Vector3.SmoothDamp(transform.position, goalPos, ref velocity, smoothTime);

    }
}
