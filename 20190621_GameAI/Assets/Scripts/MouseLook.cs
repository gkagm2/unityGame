using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

    public float sensitivity = 700.0f;
    public float rotationX;
    public float rotationY;

	void Update () {
        float mouseMoveValueX = Input.GetAxis("Mouse X");

        float mouseMoveValueY = Input.GetAxis("Mouse Y");

        rotationY += mouseMoveValueX * sensitivity * Time.deltaTime;
        rotationX += mouseMoveValueY * sensitivity * Time.deltaTime;

        // 상하
        if(rotationX > 90f)
            rotationX = 90f;
        if (rotationX < -90f)
            rotationX = -90f;

        transform.eulerAngles = new Vector3(-rotationX, rotationY, 0.0f);
        print(transform.eulerAngles.x + " , " + transform.eulerAngles.y + ", " + transform.eulerAngles.z);
	}
}
