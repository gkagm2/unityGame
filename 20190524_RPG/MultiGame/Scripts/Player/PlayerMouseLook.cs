using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseLook : MonoBehaviour {

    public float sensitivity = 500.0f;
    public float rotationX;
    public float rotationY;

    public float limitUpY;
    public float limitDownY;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float mouseMoveValueX = Input.GetAxis("Mouse X");
        float mouseMoveValueY = Input.GetAxis("Mouse Y");

        rotationX += mouseMoveValueX * sensitivity * Time.deltaTime;
        rotationY += mouseMoveValueY * sensitivity * Time.deltaTime;


        //마우스 앞으로 이동
        if (rotationY > limitUpY)
            rotationY = limitUpY;
        //마우스 뒤로 이동
        if (rotationY < -limitDownY)
            rotationY = -limitDownY;
        transform.eulerAngles = new Vector3(-rotationY, rotationX, 0.0f);
    }
}
