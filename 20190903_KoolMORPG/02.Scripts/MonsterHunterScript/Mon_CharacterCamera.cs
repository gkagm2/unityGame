using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mon_CharacterCamera : MonoBehaviour
{
    public GameObject targetObject; // When idle state, Target object
    public Mon_CharacterController characterController;

    public float speedH = 2.0f; // Horizontal speed
    public float speedV = 2.0f; // Vector speed

    public float maxPitch = 90; // Max rotation pitch
    public float minPitch = -50;// Min rotation pitch

    [HideInInspector]
    public float yaw = 0.0f;    // Yaw angle
    [HideInInspector]
    public float pitch = 0.0f;  // Pitch angle

    // Touch movement
    private Vector3 nowPos, prePos;
    private Vector3 movePos;
    private float nowPosX, prePosX;
    private float movePosX;
    public float moveSpeedTouch = 30.0f;

    private void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        //Mouse();
        TouchMobileScreen();
    }

    private void Mouse()    
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        // limit pitch angle
        if (pitch >= maxPitch)
        {
            pitch = maxPitch;
        }
        if (pitch <= minPitch)
        {
            pitch = minPitch;
        }
        targetObject.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

    public void TouchMobileScreen()
    {
        // 터치 작동
        if (Input.touchCount >= 1)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 tpos = touch.position; // 터치 좌표를 가져온다.
            if (tpos.x >= Screen.width / 2) // 오른쪽 화면영역일 때 
            {
                if (touch.phase == TouchPhase.Began)
                {
                    prePos = touch.position - touch.deltaPosition;
                    //prePosX = touch.position.x - touch.deltaPosition.x;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    nowPos = touch.position - touch.deltaPosition;
                    //nowPosX = touch.position.x - touch.deltaPosition.x;
                    movePos = (prePos - nowPos) * moveSpeedTouch * Time.deltaTime;
                    //movePosX = (prePosX - nowPosX) * moveSpeedTouch * Time.deltaTime;

                    targetObject.transform.eulerAngles = new Vector3(movePos.x, movePos.y, 0.0f);
                    //targetObject.transform.eulerAngles = new Vector3(movePosX, yaw, 0.0f);
                    prePos = touch.position - touch.deltaPosition;
                    //prePosX = touch.position.x - touch.deltaPosition.x;
                }
            }
            Debug.Log("오른쪽 화면 영역을 클릭하고 있음. : " + tpos.x + ", " + tpos.y);
            
        }
    }
}
