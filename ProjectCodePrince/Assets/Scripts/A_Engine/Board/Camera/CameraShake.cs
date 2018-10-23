using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    float yPos;
    float shakeAmount = 0.2f ;

    public Transform myCam;
    private bool flip;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) StartCoroutine("CamShake");
    }

    IEnumerator CamShake()
    {
        yPos = shakeAmount;

        while (Mathf.Abs(yPos) > 0.01f)
        {
            yPos = Mathf.Abs(yPos) - 0.1f;

            if (flip) yPos *= -1;

            flip = !flip;

            //Tripple setup thingo!
            Vector3 tempPos = myCam.localPosition;
            tempPos.x = yPos;
            myCam.localPosition = tempPos;

            yield return null;
        }
    }
}
