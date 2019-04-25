using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideEnemyControl : MonoBehaviour {

    EnemyInfo enemyInfo;

    //밑까지 내려가는지 
    public float limitDownpositionY;

    public float speed;

    public float limitDistanceX; //움직일 수 있는 최대 거리
    SideEnemyRespawner sideEnemyRespawner;
	// Use this for initialization
	void Start () {
        sideEnemyRespawner = GameObject.Find("SideEnemyRespawner").GetComponent<SideEnemyRespawner>();
        enemyInfo = GetComponent<EnemyInfo>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!GameManager.isGameOver)
        {
            //움직여라
            Move(sideEnemyRespawner.direction);

            //제한 거리까지 가면 없애기
            DestroyWhenLimitDistanceX(sideEnemyRespawner.direction);
        }
    }
    public void Move(bool direction)
    {
        float moveAmt = speed * Time.deltaTime;
        if (direction) //왼쪽에서 오른쪽이면
        {
            transform.Translate(moveAmt, 0, 0); //오른쪽으로 이동
        }
        else //오른쪽에서 왼쪽이면
        {
            transform.Translate(-moveAmt, 0, 0); //왼쪽으로 이동
        }
    }


    public void DestroyWhenLimitDistanceX(bool direction)
    {
        //거리제한까지 가면
        if(direction) //왼쪽에서 오른쪽이면
        {
            if (transform.position.x > limitDistanceX) //오른쪽 끝까지 가면
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (transform.position.x < -limitDistanceX) //왼쪽 끝까지 가면
            {
                Destroy(gameObject);
            }
        }
        
    }
    public void DoSmallScaleY(float ySize)
    {

        Debug.Log("ySize :" + ySize);
        transform.localScale -= new Vector3(0, ySize, 0);
    }
    //적이 땅에 닿으면
    public void DestroyWhenCloseLimitPositionY()
    {
        if (transform.position.y < limitDownpositionY)
        {
            GameManager.isGameOver = true; //게임 종료
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet")
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
            
        }
    }
}
