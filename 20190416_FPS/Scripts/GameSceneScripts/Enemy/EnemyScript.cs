using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    
    /// Enemy info
    public float speed = 5.0f;               //움직이는 속도
    public float rotationSpeed = 10.0f;      // 도는 속도
    public float attackableRange = 2.5f;     //Attack 할 수 있는 거리
    public float attackStateMaxTime = 2.0f;  //Attack하는 최대 시간
    public int score = 10;                   //적 파괴에 대한 점수 처리
    public int hp = 3;                       //Enemy 체력
    public enum EnemyState                   // 상태
    {
        none = -1, idle =0, move, attack, damage, dead
    }
    public EnemyState enemyState;            //상태 선언(열거형)


    float stateTime = 0.0f; // 상태 쿨타임
    public float idleStatemaxTime = 2.0f; //상태 최대 쿨타임
    public float damageStateMaxTime = 2.0f;

    /// Component
    Transform target; // 적의 이동 상태를 구현하기위한 멤버 변수
    CharacterController characterController;//캐릭터 컨트롤러
    PlayerState playerState;                //PlayerState 가져옴
    PlayerSound playerSound;                //PlayerSound 가져옴
    

    /// Prefebs
    public GameObject deadEffect;       // 죽을 때 Effect

    /// Sound
    public AudioClip hitSound;          // 맞을때 나는 소리


    private void Awake()
    {
        enemyState = EnemyState.idle;               //상태는 idle 바꿈
        GetComponent<Animation>().Play("idle02");   //애니메이션은 idle로 바꿈

    }
    // Use this for initialization
    void Start () {
        target = GameObject.Find("Player").transform;             //Player의 Transform을 가져옴
        characterController = GetComponent<CharacterController>();//캐릭터 컨트롤러를 가져옴
        playerState = target.GetComponent<PlayerState>();         //Player State 가져옴
        playerSound = target.GetComponent<PlayerSound>();         //Player Sound 가져옴 
    }
	
	// Update is called once per frame
	void Update () {
        switch (enemyState) //상태
        {
            case EnemyState.idle: // idle일 경우
                //stateTime += Time.deltaTime;
                //if (stateTime > idleStatemaxTime)
                //{
                //    stateTime = 0.0f;
                //}
                enemyState = EnemyState.move; //상태를 move로 바꾼다.
                break;
            case EnemyState.move: // move일 경우
                GetComponent<Animation>().Play("walk03"); //걷는 애니메이션으로
                GetComponent<Animation>()["walk03"].speed = 4.0f;

                float distance = Vector3.Distance(target.position, transform.position); // Player와 Enemy의 거리를 구함.

                if (distance < attackableRange) //Player와 Enemy의 거리가 Enemy의 공격범위 안이면
                {
                    enemyState = EnemyState.attack; //Attack으로 상태가 바뀜
                    stateTime = attackStateMaxTime; //약간의 시간을 두고 공격하는 문제점을 보완
                }
                else //거리가 Enemy의 공격범위 밖이면
                {
                    Vector3 direction = target.position - transform.position; //플레이어에서 타겟을 뺀 Vector3
                    direction.y = 0.0f; //y축은 0으로 둔다.
                    direction.Normalize(); //벡터의 크기를 1로 만든다.
                    characterController.SimpleMove(direction * speed); //속도를 이용하여 움직인다.

                    //TODO : 모르겠다 알아봐라
                    //Lerp 보간 
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
                }
                break;

            case EnemyState.attack:
                stateTime += Time.deltaTime;
                if (stateTime > attackStateMaxTime) //때리는 쿨타임
                {
                    stateTime = 0.0f;
                    //Enemy가 공격후 플레이어를 따라와서 공격하는 코드 구현
                    float distance1 = (target.position - transform.position).magnitude; //벡터의 길이를 반환한다.
                    if (distance1 > attackableRange)
                    {
                        enemyState = EnemyState.move;
                    }
                    else
                    {
                        //Debug.Log("Occur!!!");
                        GetComponent<Animation>().Play("atack02");
                        GetComponent<Animation>().PlayQueued("idle02", QueueMode.CompleteOthers); //idle상태가 끝나고 나서 atack02 돌려라

                        playerState.DamageByEnemy(); //플레이어 피격함수 호출
                        playerSound.HitSoundByEnemy(); //맞을 때 플레이어의 소리 호출

                    }
                }
        
                break;
            case EnemyState.damage:
                
                --hp;
                if (hp <= 0)
                {
                    AudioManager.Instance().PlaySfx(hitSound); //맞는 소리
                    enemyState = EnemyState.dead;
                    break;
                }
                stateTime += Time.deltaTime;
                if (stateTime > damageStateMaxTime)
                {

                    AudioManager.Instance().PlaySfx(hitSound); //맞는 소리
                    Debug.Log("hp : " + hp);
                    // TOOD :   애니메이션이 씹힌다..
                    //GetComponent<Animation>()["damage"].speed = 0.5f;
                    GetComponent<Animation>().Play("damage");


                    stateTime = 0.0f;
                    enemyState = EnemyState.idle;

                }
                
                break;
            case EnemyState.dead:


                GetComponent<AudioSource>().Stop(); // 비명을 멈춤
                StartCoroutine(DeadProcess());
                enemyState = EnemyState.none;

                ScoreManager.Instance().myScore += score;
                break;
        }
	}
    IEnumerator DamageProcess()
    {


        yield return new WaitForSeconds(GetComponent<Animation>()["damage"].length);
    }

    IEnumerator DeadProcess()
    {
        GetComponent<Animation>()["death02"].speed = 0.5f;
        GetComponent<Animation>()["death02"].wrapMode = WrapMode.ClampForever; //한번 플레이하고 애니메이션 마지막 유지 // (1)
        GetComponent<Animation>().Play("death02"); // 죽는 애니메이션 플레이

        //WrapMode.ClampForever
        yield return new WaitForSeconds(5);//5초후에 사라짐

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

        //Debug.Log("폭탄에 맞았다" + collision.gameObject.name);
        if (enemyState == EnemyState.dead) //죽으면 끝
        {
            //Debug.Log("죽었다!");
            return;
        }

        //Bomb일때
        int layerIndex = collision.gameObject.layer; //Bomb
        if (layerIndex != 10)  //이것도 같다.
            return;
        //Debug.Log("데미지 받음!");
        enemyState = EnemyState.damage; //damage 상태로 바꾼다.
    }
    public void ApplyHitEffect(RaycastHit hit)
    {
        GameObject effect = Instantiate(deadEffect, hit.point, Quaternion.LookRotation(hit.normal)) as GameObject;
        Destroy(effect, 2f);
    }


}
