using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

    public float sensitivity = 700.0f;
    public float rotationX;
    public float rotationY;

    //플레이어 죽음 상태 처리
    PlayerState playerHelth = null;

	// Use this for initialization
	void Start () {
        playerHelth = transform.parent.GetComponent<PlayerState>();
    }
	
	// Update is called once per frame
	void Update () {
        if (playerHelth.isDead) //플레이어가 죽으면
            return;
        
        float mouseMoveValueX = Input.GetAxis("Mouse X");
        //Debug.Log("X  : "+ mouseMoveValueX);
        float mouseMoveValueY = Input.GetAxis("Mouse Y");
        //Debug.Log("Y  : " + mouseMoveValueY);
        rotationY += mouseMoveValueX * sensitivity * Time.deltaTime;
        rotationX += mouseMoveValueY * sensitivity * Time.deltaTime;

        //상하
        if (rotationX > 90.0f)
            rotationX = 90.0f;
        if (rotationX < -90.0f)
            rotationX = -90.0f;
        //좌우
        //if (rotationY > 90.0f)
        //    rotationY = 90.0f;
        //if (rotationY < -90.0f)
        //    rotationY = -90.0f;

        transform.eulerAngles = new Vector3(-rotationX, rotationY, 0.0f);


	}
}
