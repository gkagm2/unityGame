using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour {
    public GameObject ground;
    
    public float speed = 2.0f;



	void Update () {
        //Y축으로 움직인다(World좌표 기준)
        transform.Translate(new Vector3(0.0f, speed * Time.deltaTime, 0.0f), Space.World);
        
        //게임 오브젝트 이름이 hell이면
        if (ground.gameObject.name == "hell")
        {
            //위 아래로 움직이는 Control Code
            if (transform.position.y > 0.34)
            {
                speed = -speed;
            }else if(transform.position.y < -1.16)
            {
                speed = -speed;
            }

            //게임 오브젝트 이름이 bigHell이면
        } else if (ground.gameObject.name == "bigHell")
        {
            //위 아래로 움직이는 Control Code
            if (transform.position.y > 13)
            {
                speed = -speed;

            }
            else if (transform.position.y < 4.5)
            {
                speed = -speed;
            }
        }

    }
}
