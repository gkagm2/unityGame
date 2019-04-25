using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public float speed = 15.0f;
    public float distance = 4.0f;

    public GameObject bulletObject;
    PlayerInfo playerInfo;

    // Use this for initialization
    void Start() {
        playerInfo = GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void Update() {
        if (!GameManager.isGameOver)
        {
            MyPlayerControl();
        }


        //TODO Mobile
        InputMobile();

    }
    public void MyPlayerControl()
    {
        float moveAmt = Time.deltaTime * speed;

        if(Input.GetKeyDown(KeyCode.Space)){
            Shoot(moveAmt);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if(transform.position.x < -distance)
            {
                PlayerMove(-moveAmt); //반대로 움직여라
            }else
                PlayerMove(moveAmt); //
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if(transform.position.x > distance)
            {
                PlayerMove(moveAmt);

            }else
                PlayerMove(-moveAmt);
        }
    }
    // 움직이기
    public void PlayerMove(float moveAmt)
    {
        transform.Translate(Vector3.left * moveAmt);
    }

    //총알 쏘기
    public void Shoot(float moveAmt)
    {
        //총알 생성.
        Instantiate(bulletObject, transform.position, transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyBullet")
        {
            playerInfo.hp -= 1;
            if(playerInfo.hp <= 0)
            {
                GameManager.isGameOver = true; //게임종료 후
                Destroy(gameObject);
            }

            //사이즈 줄이기
            DoSmallScaleY((float)playerInfo.fullhp * 0.01f); //사이즈 줄인다.
            if (transform.localScale.y <= 0) //사이즈가 다 줄어들었으면
            {
                GameManager.isGameOver = true; //게임종료 후
                Destroy(gameObject); //파괴 됨
            }
        }
    }
    public void DoSmallScaleY(float ySize)
    {

        //Debug.Log("ySize :" + ySize);
        transform.localScale -= new Vector3(0, ySize, 0);
    }

    //모바일 입력
    public void InputMobile()
    {
        var fingerCount = 0;
        foreach(Touch touch in Input.touches)
        {
            if(touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
            {
                fingerCount++;
            }
        }
        if(fingerCount > 0)
        {
            print("User has " + fingerCount + " finger(s) touching the screen");
        }
    }
}
