using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    EnemyInfo enemyInfo;

    public bool direction = true; //true면 왼쪽으로 가는중 false면 오른쪽으로 가는중
    public float speed; // 좌우 속도
    float positionX;

    public float downSpeed; // 아래로 내려가는 속도
    


    public float distance = 8.0f;

    //맞았을 때 위로 올라가는 충격량
    public float whenhitMoveUpAmt;



    //밑까지 내려가는지 
    public float limitDownpositionY;


    //GameManager gameManager;
    // Use this for initialization
    void Start()
    {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyInfo = GetComponent<EnemyInfo>();
        speed = Random.Range(3.0f, 5.0f);

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isGameOver) //게임이 진행중이면
        {
            float moveAmt = speed * Time.deltaTime; //좌우 속도.
            float moveDownAmt = downSpeed * Time.deltaTime; //아래로 내려가는 속도

            // 범위 체크
            if (direction == true) //왼쪽으로 갈 때
            {
                if (transform.position.x < positionX) //위치값까지 갔으면
                {
                    ChangePosition(direction); //새로 갈 위치값을 생성한다.
                    DirectionChange(); //방향을 바꿈.
                }

            }
            else // 오른쪽으로 갈 때
            {
                if (transform.position.x > positionX) //위치값까지 갔으면
                {
                    ChangePosition(direction); //새로 갈 위치값을 생성한다.
                    DirectionChange(); //방향을 바꾼다.
                }
            }

            
            SideMove(moveAmt); //좌 우 움직임
            DownMove(moveDownAmt); // 아래로 내려가는 움직임
            DestroyWhenCloseLimitPositionY(); // 적이 한정 Y posotion 거리까지 내려가면 없애기

        } //Game over면
        else
        {

        }

    }
    //적이 땅에 닿으면
    public void DestroyWhenCloseLimitPositionY()
    {
        if(transform.position.y < limitDownpositionY)
        {
            GameManager.isGameOver = true; //게임 종료
        }
    }
    public void SideMove(float moveAmt)
    {
        if(direction == true) //왼쪽으로 가는 상태면
        {
            transform.Translate(Vector3.left * moveAmt); //왼쪽으로 이동
        }
        else //오른쪽으로 가는 상태면
        {
            transform.Translate(Vector3.right * moveAmt); //오른쪽으로 이동
        }
    }
    public void DownMove(float moveAmt)
    {
        transform.Translate(Vector3.down * moveAmt); //아래로 이동
    }


    public void ChangePosition(bool direction)
    {
        int random = Random.Range(0, (int)distance);
        //왼쪽이였으면
        if(direction == true)
        {
            positionX = random; //오른쪽 위치의 값으로
        }
        else //오른쪽이면
        {
            positionX = -random; //왼쪽 위치의 값으로
        }
    }
    // 움직이기
    public void EnemyMove(float moveAmt)
    {
        transform.Translate(Vector3.left * moveAmt);
    }


    public void DirectionChange()
    {
        if (direction)
            direction = false;
        else
            direction = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerBullet")
        {
            enemyInfo.hp -= 1;      //체력이 1 담
            if (enemyInfo.hp <= 0)  //hp가 0이면
            {
                Destroy(gameObject);//파괴
            }

            //사이즈 줄이기
            DoSmallScaleY((float)enemyInfo.fullhp * 0.01f); //사이즈 줄인다.
            if (transform.localScale.y <= 0) //사이즈가 다 줄어들었으면
            {
                Destroy(gameObject); //파괴 됨
            }
            MoveUpWhenHit(); //맞았을 때 위로 올라감
        }
    }
    public void DoSmallScaleY(float ySize)
    {

        Debug.Log("ySize :" +ySize);
        transform.localScale -= new Vector3(0, ySize, 0);
    }
    //맞았을때 위로 올라감
    public void MoveUpWhenHit() {
        transform.position += new Vector3(0, whenhitMoveUpAmt, 0);
    }
}
