using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : BallController {

    public float moveSpeed = 10.0f;


    // Update is called once per frame
    void Update() {
        if (BallGameManager.instance.isPlayerCatched)
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

}
