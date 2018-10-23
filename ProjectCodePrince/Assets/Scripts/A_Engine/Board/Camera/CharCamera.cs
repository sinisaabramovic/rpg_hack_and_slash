using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCamera : MonoBehaviour {

    float dampTime = 0.2f; //offset from the viewport center to fix damping
    Vector3 velocity;
    Transform target;
    public float up_distance = 5f;
    public float localRotAngle = -45f;
    // Use this for initialization
    void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        velocity = Vector3.zero;
    }
	
    public void setUpDistnce(float distance){
        up_distance = distance;
    }

	// Update is called once per frame
	void Update () {
        if (target)
        {
            Vector3 point = Camera.main.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z + Mathf.Lerp(up_distance, point.z, 0.1f)));
            delta.y -= 2.5f;
            Vector3 destination = transform.position + delta;

            // Set this to the Y position you want the camera locked to
            //destination.z = up_distance;
            destination.z = Mathf.Lerp(up_distance, destination.z, 0.2f);

            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);

            if(transform.position.z > 10f){
                transform.position = new Vector3(transform.position.x, transform.position.y, 10f);
            }

        }
        //transform.localRotation = Quaternion.Euler(0.0f, 0.0f, localRotAngle);

        if(transform.eulerAngles.z > localRotAngle){
            //transform.Rotate(45f * Vector3.forward * Time.deltaTime);    
        }

        Debug.Log(transform.eulerAngles.z);
    }

    public void RotateCamera(float angle){
        //this.transform.localRotation.Set(0f, 0f, angle, 0f);
        //transform.localRotation = Quaternion.Euler(0.0f, 0.0f, angle);
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
        localRotAngle = angle;
    }
}
