using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {
    public int enemyReuseCount = 5; //몇 번 재사용 할 것인지
    float speed;
    public float carSpeed = 1.0f;
    public bool isCollisionToPlayer = false;
    Vector3 resetPosition;

    GameManager gameManager;


    //AudioSource audioSource;
    //public AudioClip bombSound;

    // Use this for initialization
    void Start () {
        resetPosition  = transform.position; //현재 포지션을 저장해 둠

        //게임 매니저 불러옴
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); 


        //audioSource = GameObject.Find("enemy").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        //speed = GameObject.Find("Ground").GetComponent<GroundScrolling>().scrollSpeed;

        //게임 종료가 아니면
        if (!gameManager.gameOver)
        {
            //적 속도 계산
            speed = GameObject.Find("Ground").GetComponent<GroundScrolling>().scrollSpeed * carSpeed;

            MoveEnemy(); //enemy 이동
            
            

            //적 포지션 재이동
            if (transform.position.z < -6.5f)
            {
                //재사용 횟수가 끝나면
                if (enemyReuseCount == 0)
                {// 이 오브젝트를 없앤다.
                    Destroy(gameObject);
                }
                else //재사용 횟수가 끝나지 않았으면 
                {
                    //이전 위치로 이동
                    transform.position = resetPosition;
                    --enemyReuseCount; //재사용 횟수 1 감소

                }

            }
        }
        else
        {

        }
        
	}

    private void OnTriggerEnter(Collider other)
    {
        //플레이어와 부딫히면
        if(other.tag == "Player")
        {
            //원래 소리가 나야 하는데 바로 밑에 Destroy 때문에 씹힘.
            //audioSource.PlayOneShot(bombSound);
            isCollisionToPlayer = true;
            
            
            ////속도를 줄인다.
            //gameManager.scrollSpeed = 0.1f;
            ////처음 속도로 돌아감
            //gameManager.gameFlow = 0;

            //enemy는 2초 후 파괴
            Destroy(gameObject,2);

        }
        //똑같은 enemy면  겹치지않게 삭제시킴
        if(other.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
    //enemy 이동
    void MoveEnemy()
    {
        if (isCollisionToPlayer) //플레이어와 충돌 했으면
        {
            //충돌하는 위치로 이동
            transform.Translate(new Vector3(0.0f, Random.Range(0.1f, 0.2f), 0.0f), Space.World);
            transform.Rotate(Random.Range(5, 10), Random.Range(5, 10), Random.Range(5, 10));
        }
        else // 플레이어와 충돌하지 않았으면
        {
            //적 이동
            transform.Translate(new Vector3(0.0f, 0.0f, -speed), Space.World);
        }
    }

}
