using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    

    // 애니메이션 열거형
    public enum EnemyState
    {
        none = -1,
        idle =0,
        move,
        attack,
        damage,
        dead
    }
    public EnemyState enemyState = EnemyState.idle;


    // 애니메이션을 하귕한 쿨타임
    float stateTime = 0.0f;
    public float idleStatemaxTime = 2.0f;


    // 적의 이동 상태를 구현하기위한 멤버 변수
    Transform target;

    public float speed = 5.0f;
    public float rotationSpeed = 10.0f;
    public float attackableRange = 2.5f;


    //Attack
    public float attackStateMaxTime = 2.0f;


    //zombie 체력 구현
    int hp = 1;

    //PlayerState 가져옴
    PlayerState playerState;

    //캐릭터 컨트롤러
    CharacterController characterController;

    //적 파괴에 대한 점수 처리
    public int score = 10;

    private void Awake()
    {
        enemyState = EnemyState.idle;
        GetComponent<Animation>().Play("idle02");

    }
    // Use this for initialization
    void Start () {
        target = GameObject.Find("Player").transform;
        characterController = GetComponent<CharacterController>();

        //playerstate 가져옴
        playerState = target.GetComponent<PlayerState>();
	}
	
	// Update is called once per frame
	void Update () {
        switch (enemyState)
        {
            case EnemyState.idle:
                stateTime += Time.deltaTime;
                if (stateTime > idleStatemaxTime)
                {
                    stateTime = 0.0f;
                    enemyState = EnemyState.move;
                }
                break;
            case EnemyState.move:
                GetComponent<Animation>().Play("walk03");

                //magnitude  float의 형태로 바꿔줌 (벡터 사이의 거리)
                //float distance = (target.position - transform.position).magnitude;  //방법 1

                float distance = Vector3.Distance(target.position, transform.position); // 방법 2

                //print("distance : " + distance);
                ///
                /// Vector3.Distance  혹은 Vector2.Distance도 있음.
                ///
                if (distance < attackableRange)
                {
                    enemyState = EnemyState.attack;

                    //약간의 시간을 두고 공격하는 문제점을 보완
                    stateTime = attackStateMaxTime;
                }
                else
                {
                    Vector3 dir = target.position - transform.position;
                    dir.y = 0.0f;
                    dir.Normalize();
                    characterController.SimpleMove(dir * speed);
                    //Lerp 보간 
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), rotationSpeed * Time.deltaTime);
                }
                break;

            case EnemyState.attack:
                stateTime += Time.deltaTime;
                if (stateTime > attackStateMaxTime)
                {
                    stateTime = 0.0f;
                    //Enemy가 공격후 플레이어를 따라와서 공격하는 코드 구현
                    float distance1 = (target.position - transform.position).magnitude;
                    if (distance1 > attackableRange)
                    {
                        enemyState = EnemyState.move;
                    }
                    else
                    {
                        //Debug.Log("Occur!!!");
                        GetComponent<Animation>().Play("atack02");
                        GetComponent<Animation>().PlayQueued("idle02", QueueMode.CompleteOthers); //idle상태가 끝나고 나서 atack02 돌려라

                        //플레이어 피격함수 호출
                        playerState.DamageByEnemy();

                    }
                }
        
                break;
            case EnemyState.damage:
                --hp;

                GetComponent<Animation>()["damage"].speed = 0.5f;
                GetComponent<Animation>().Play("damage");

                GetComponent<Animation>().PlayQueued("idle02", QueueMode.CompleteOthers);

                stateTime = 0.0f;
                enemyState = EnemyState.idle;

                if (hp <= 0)
                {
                    enemyState = EnemyState.dead;
                }
                break;
            case EnemyState.dead:
                StartCoroutine(IDeadProcess());
                enemyState = EnemyState.none;
                //GetComponent<Animation>().Play("death02"); // 죽는 애니메이션 플레이
                //GetComponent<Animation>()["death02"].wrapMode = WrapMode.ClampForever; //한번 플레이하고 애니메이션 마지막 유지
                //Destroy(gameObject,5); //5초후에 사라짐

                ScoreManager.Instance().myScore += score;
                break;
        }
	}
    IEnumerator IDeadProcess()
    {
        GetComponent<Animation>()["death02"].speed = 0.5f;
        GetComponent<Animation>()["death02"].wrapMode = WrapMode.ClampForever; //한번 플레이하고 애니메이션 마지막 유지 // (1)
        GetComponent<Animation>().Play("death02"); // 죽는 애니메이션 플레이

        //////////////////////
        //WrapMode.ClampForever로 하려면 
        yield return new WaitForSeconds(5);//5초후에 사라짐   // (1)

        //WrapMode.Once로 하려면 
        //while (GetComponent<Animation>().isPlaying)  // 2
        //{
        //    yield return new WaitForEndOfFrame();
        //}
        //////////////////////
        
        //메모리 풀 사용 안하면
        //Destroy(gameObject);  

        //메모리 풀 사용시
        
        InitEnemy();
        gameObject.SetActive(false);

    }
    void InitEnemy()
    {
        hp = 2;
        enemyState = EnemyState.idle;
        GetComponent<Animation>().Play("idle02");
    }
    private void OnCollisionEnter(Collision collision)
    {
        //죽으면 끝
        if (enemyState == EnemyState.dead)
            return;


        /////////////////// Tag 및 Layer를 통한 충돌 감지 //////////////////////
        //Debug.Log("name : " + collision.gameObject.name);
        //if (collision.gameObject.name.Contains("Bomb") == false)  //이것도 같다.
        //    return;

        //Debug.Log("Tag : " + collision.gameObject.tag);
        //if (collision.gameObject.tag != "Bomb")                     //이것도 같다.
        //    return;

        int layerIndex = collision.gameObject.layer;
        if (layerIndex != 10)                                          //이것도 같다.
            return;
        //if (LayerMask.LayerToName(layerIndex) != "Bomb")              //이것도 같다.
        //    return;
        /////////////////////////////////////////////////////////////////////////

        enemyState = EnemyState.damage;


        //if (collision.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        //{
        //    enemyState = EnemyState.damage;
        //}
    }
}
