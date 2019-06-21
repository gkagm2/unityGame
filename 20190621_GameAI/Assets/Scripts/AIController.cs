using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {
    CharacterScript ai;
    public DetectorScript detector;

    [Tooltip("바라볼 상대방의 Object")]
    [SerializeField] GameObject targetObj;


    float t_fireCoolTime = 0;

    [Tooltip("Cool Time")]
    [SerializeField] float fireSpeed = 2.0f;


    enum AiState
    {
        idle,
        attack,
        damage,
        dead
    };
    AiState aiState;

    [Tooltip("타겟과의 거리")]
    public float distanceRange = 5.0f;


    // idle
    float turnCoolTime;
    float t_turnCoolTime = 0;
    bool turnRight = true;

    // Use this for initialization
    void Start () {
        ai= GetComponent<CharacterScript>();

        aiState = AiState.idle;
        turnCoolTime = Random.Range(2, 4);
    }

    float moveSpeed=  3.0f;
	
    
	// Update is called once per frame
	void Update () {

        if (aiState == AiState.dead)
        {
            transform.Rotate(new Vector3(0, 0, 360 * Time.deltaTime));
            return;
        }


        // 감지했으면
        if (detector.isDetect)
        {
            // 감지하고싶은 타겟과 감지한 어떤 타겟이 똑같으면
            if (targetObj.Equals(detector.detectedTargetObj))
            {
                aiState = AiState.attack;
            }
        }
        else //감지 못했으면
        {
            aiState = AiState.idle;
        }
        

        switch(aiState){
            case AiState.idle:
                 
                
                t_turnCoolTime += Time.deltaTime;
                if(t_turnCoolTime > turnCoolTime)
                {
                    turnRight = turnRight == true ? false : true;
                    turnCoolTime = Random.Range(2, 4);
                    t_turnCoolTime = 0;
                }
                if(turnRight == true)
                {
                    transform.Rotate(0, 100 * Time.deltaTime, 0);
                }
                else
                {
                    transform.Rotate(0, -100 * Time.deltaTime, 0);
                }
                

                break;
            case AiState.damage:
                Debug.Log("Damage!!");
                break;
            case AiState.attack:
                Transform fromP = transform;
                Transform toP = targetObj.transform;

                // 선형 보간. (부드럽게 회전하며 바라보게 함)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetObj.transform.position - transform.position), 5 * Time.deltaTime);
                //transform.LookAt(targetObj.transform.position);
                
                // 타겟과 나의 거리를 알아옴.
                float distance = Vector3.Distance(targetObj.transform.position, transform.position);

                if (distance < 6)
                {
                    Fire();
                }
                else if (distance < 15)
                {
                    // 총알 발사
                    Fire();
                    // 적방향으로 움직임.
                    Move(targetObj);
                }
                else
                {
                    // 적방향으로 움직임.
                    Move(targetObj);
                }
                break;
        }
        
	}

    // 움직인다.
    public void Move(GameObject targetObj)
    {
        transform.Translate(0, 0, moveSpeed * Time.deltaTime);
    }


    // 총알 발사
    public void Fire()
    {
        t_fireCoolTime += Time.deltaTime;
        if (t_fireCoolTime > fireSpeed)
        {
            ai.Fire();
            t_fireCoolTime = 0;
        }
    }
    public void HitByBullet()
    {
        --ai.hp;
        aiState = AiState.damage;
        if (ai.hp <= 0)
        {
            Debug.Log("죽었다.!");
            aiState = AiState.dead;
            
            Destroy(gameObject, 3f);

        }
    }

    // 길찾기 알고리즘
    public void SearchPathtoTarget()
    {
        // TODO : use A* algorithm

    }

}
