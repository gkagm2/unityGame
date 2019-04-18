using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    [Header("Booster")]
    // 부스터 
    public bool haveBooster = false; //부스터를 가졌는지 확인
    public bool boosterOneShootFlag = true; // 부스터를 한번만 발동 시키게 하기
    public bool boosterOn = false; // 부스터가 켜져 있는지
    public float boosterCoolTime=0; // 부스터의 쿨타임
    public float boosterDelayTime = 5.0f; //부스터의 지연 시간

    [Header("Enemy respawner")]
    EnemyRespawner enemyRespawner;

    [Header("Game object")]
    GameManager gameManager;

    // player info
    public int carCrashCount = 0; //플레이어가 적과 박은 횟수
    public GameObject []carFrame; //자동차 뼈대

    // effect
    public GameObject explosionEffect; //자동차 터질 시 폭발 이펙트

    // player left, right movement
    public float moveLimit = 4.0f; //플레이어 좌우 움직임

    // player speed
    public float beforeSpeed; //현재 속도를 담을 임시 속도
    public float speed =3.0f;//속도

    /// <summary>
    /// TODO : camera control 따로 떼서 붙이는게 좋을 듯 함.
    /// </summary>
    /// 
    [Header("Player camera control")]
    // camera control
    float shakeCool;                //쿨타임
    float shakeTime;                //카메라 지연 시간
    bool shakeOn = false;           //흔들리는지 여부 판단
    public Transform cameraTrans;   //카메라 받아오기


    void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyRespawner = GameObject.Find("EnemyRespawner").GetComponent<EnemyRespawner>();
	}

    void Update()
    {
        PlayerMoveControl(); //플레이어 움직임
        CollisionCar();
        if (!boosterOn)
        {
            PlayerHp();
            Debug.Log("PlayerHp 시작" + "boosterOn : " + boosterOn);

        }
        //Debug.Log("booster on : " + boosterOn);

        //부스터가 On이면
        if (boosterOn)
        {
            if (boosterOneShootFlag == true) //부스트 한번만 사용하는 플레그 On
            {
                beforeSpeed = gameManager.scrollSpeed; //이전 스피드를 beforeSpeed에 저장
                boosterOneShootFlag = false; //플래그 off
            }
            boosterCoolTime += Time.deltaTime;//해당 시간동안
            gameManager.scrollSpeed = 20.0f; //20.0f의 속도로
            enemyRespawner.delayTime = 0.1f; //enemy 생성 속도 빠르게 함.

            if (boosterCoolTime > boosterDelayTime) //부스터 사용이 끝나면
            {
                enemyRespawner.delayTime = enemyRespawner.beforeDelayTime;//이전 속도로 바꿔줌.
                if (beforeSpeed != 0)
                {
                    gameManager.scrollSpeed = beforeSpeed; //이전 속도로 바꿈
                }
                Debug.Log("BoosterOn : " + boosterOn);
                boosterOneShootFlag = true;
                boosterOn = false;
                boosterCoolTime = 0;
            }
        }
    }
    //enenmy와 플레이어와 충돌하면 
    void CollisionCar()
    {
        //카메라를 흔든다.
        if (shakeOn == true)
        {
            shakeTime += Time.deltaTime;
            float x = Random.Range(0.0f, 0.5f);
            float y = Random.Range(2.0f, 4.0f);
            cameraTrans.position = new Vector3(x, y, -5);

            if (shakeTime > shakeCool)
            {
                shakeOn = false;
                cameraTrans.position = new Vector3(0, 3, -5);
                
                shakeTime = 0;
            }

        }
    }

    void PlayerMoveControl()
    {
        float moveAmt = speed * Time.deltaTime; //움직임 세팅

        ///플레이어 키

        //좌
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * moveAmt);
        }//우
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * moveAmt);
        }
        //부스터
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //부스터가 있으면
            if (haveBooster == true) 
            {
                //부스터를 킨다.
                boosterOn = true;//부스터가 온이 됨.
                haveBooster = false;//부스터를 없앰
            }
        }
        

        //제한 범위 설정
        if (transform.position.x > moveLimit) //x가 4보다 크면
        {//왼쪽으로
            transform.Translate(Vector3.left * moveAmt);
        }
        if (transform.position.x < -moveLimit) //-4보다 작으면 
        {//오른쪽으로
            transform.Translate(Vector3.right * moveAmt);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            ++carCrashCount;
            shakeOn = true;
        }
    }

    void PlayerHp()
    {
        /// TODO : 이거 리펙토링 각
        switch (carCrashCount)
        {
            case 1:
                carFrame[0].SetActive(false);
                break;
            case 2:
                carFrame[1].SetActive(false);

                break;
            case 3:
                carFrame[2].transform.rotation = Quaternion.Euler(0.0f, 0.0f, 15.0f);

                break;
            case 4:
                carFrame[2].SetActive(false);
                break;
            case 5:
                Instantiate(explosionEffect, transform.position, transform.rotation);
                gameManager.gameOver = true;
                Destroy(gameObject);
                break;
        }
    }
    //Hp 보너스 받기
    public void RepareCar()
    {

        // 자동차가 부서져있으면 
        if(carCrashCount != 0)
        {
            //자동차 프레임을 모두 보여주게 한다.
            carFrame[0].SetActive(true);
            carFrame[1].SetActive(true);
            carFrame[2].SetActive(true);
            //각도를 원래대로 돌아오게 한다
            carFrame[2].transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            carCrashCount = 0; //카운트를 0으로 리셋한다.
        }
    }
}
