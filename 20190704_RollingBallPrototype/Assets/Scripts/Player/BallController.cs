using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallController : MonoBehaviour {

    

    //public Vector2 nowPos, prePos;
    public float nowPosX, prePosX;
    public float movePosX;
    public float moveSpeedTouch = 30.0f;
    public float moveSpeedKeyboard = 270.0f;

    void Update()
    {
        if (BallGameManager.instance.isPlayerFail)
            return;

        SideMove();
        SideMoveTouch();
    }
    

    // 양쪽을 움직이기 (touch)
    public void SideMoveTouch()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                prePosX = touch.position.x - touch.deltaPosition.x;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                nowPosX = touch.position.x - touch.deltaPosition.x;
                movePosX = (prePosX - nowPosX) * moveSpeedTouch * Time.deltaTime;

                transform.parent.Rotate(new Vector3(0, 0, movePosX));
                prePosX = touch.position.x - touch.deltaPosition.x;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
            }
        }
    }



    // 양쪽을 움직이기 (keyboard)
    public void SideMove()
    {
        float dic_x = Input.GetAxis("Horizontal");
        dic_x = moveSpeedKeyboard * Time.deltaTime * dic_x;
        transform.parent.Rotate(new Vector3(0, 0, -dic_x));
    }



    // 벽에 부딪히면
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Wall")
        {
            BallGameManager.instance.FailGame(); // 게임 실패
        }
        else if (other.tag == "BoostItem")
        {
            // TODO :  전송(item종류 ) -> string형식으로
            Destroy(other.gameObject);
            Debug.Log("Get Boolst Item ");
        }
        else if(other.tag == "Coin")
        {
            BallGameManager.instance.GetCoin(WhereGetCoinStatus.InTheGame); // 코인 획득
            Destroy(other.gameObject);
            Debug.Log("Get Coin ");
        }
    }
}
