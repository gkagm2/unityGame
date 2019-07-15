using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {


    public float moveSpeed = 10.0f;


    // Update is called once per frame
    void Update()
    {
        if (BallGameManager.instance.isPlayerFail)
            return;
        SideMove();
    }

    // 양쪽을 움직이기
    public void SideMove()
    {
        float dic_x = Input.GetAxis("Horizontal");
        dic_x = moveSpeed * Time.deltaTime * dic_x;
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
