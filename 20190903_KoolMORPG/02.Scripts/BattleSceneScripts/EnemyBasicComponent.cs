using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBasicComponent : MonoBehaviour
{
    public float moveSpeed;         //스피드
    public float distance;          //거리
    public float attackableDis;     //공격가능 범위
    public float findDis;           //플레이어 인지 가능 범위
    public float rotSpeed = 4.50f;  //회전 속도
    public float deathTime = 1f;
    public float collOnTime = 0.2f;

    public float atk = 100f;               //공격력
    public float def = 100f;               //방어력
    public float hp = 100f;         //체력
    public float basicAtk = 100f;
    public float basicDef = 100f;
    public float basicHp = 100f;

    public bool isMove = false;
    public bool isAttack = false;
    public bool isDamaged = false;
    public bool isDeath = false;
    public bool isFind = false;

    public ParticleSystem ps;
    public AudioSource damagedSound;
    public SphereCollider enemyWeapon;
    public StageManager stageManager;
    public PlayerMovement pm;

    private Transform playerTr = null;
    private Transform tr;
    public NavMeshAgent nma;

    public enum EEnemyState
    {
        find,
        idle,
        move,
        damage,
        attack,
        death
    }
    public EEnemyState eEnemyState;
   
    /// <summary>
    /// 초기화 함수
    /// </summary>
    public void MonsterInitiallize()
    {
        pm = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        enemyWeapon = GetComponentsInChildren<SphereCollider>()[1];
        nma = GetComponent<NavMeshAgent>();
        tr = GetComponent<Transform>();
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        /*stageNumber = StageData.stageNumber;*/  //테스트용
        //level = pm.level;
    }
    
    /// <summary>
    /// 방향 회전
    /// </summary>
    public void EnemyRotate()
    {
        if (eEnemyState.Equals(EEnemyState.idle)||eEnemyState.Equals(EEnemyState.death)) return;
        tr.rotation = Quaternion.Lerp(tr.rotation, Quaternion.LookRotation(new Vector3(playerTr.position.x, 0, playerTr.position.z) - new Vector3(tr.position.x, 0, tr.position.z)), rotSpeed * Time.deltaTime);
    }
    
    /// <summary>
    /// 거리 계산
    /// </summary>
    public void DistanceCheck()
    {
        distance = Vector3.Distance(tr.position, playerTr.position);
    }

    /// <summary>
    /// 플레이어와 가까워질 시 플레이어에게로 이동
    /// </summary>
    /// <param name="_findDis"></param>

    public void FindPlayer(float _findDis)
    {
        if(distance <= _findDis)
        {
            if(!isFind)
            {
                pm.target.Add(gameObject);
                isFind = true;
            }
            nma.speed = moveSpeed;
            isMove = true;
        }
    }

    /// <summary>
    /// 플레이어에게 이동 및 공격 가능 범위 접근 시 공격
    /// </summary>
    /// <param name="_attackableDis"></param>
    public void MoveToPlayer(float _attackableDis)
    {
        nma.destination = playerTr.position;
        if(distance <= _attackableDis)
        {
            nma.isStopped = true;
            isMove = false;
            isAttack = true;
        }
    }

    /// <summary>
    /// 플레이어 재 추격
    /// </summary>
    /// <param name="_attackableDis"></param>
    public void ChasingPlayer(float _attackableDis)
    {
        if(distance >= attackableDis)
        {
            isMove = true;
            isAttack = false;
            nma.isStopped = false;
        }
    }
    
    public void DamagedCalculation()
    {
        hp -= pm.atk - (def * 0.5f);
    }

    public void MonsterRecycle()
    {
        hp = basicHp + (pm.level * basicHp) + (stageManager.stageIdx * basicHp);
        atk = basicAtk + (pm.level * basicAtk);
        def = basicDef + (pm.level * basicDef);

        isMove = false;
        isAttack = false;
        isDamaged = false;
        isDeath = false;
        isFind = false;
    }

    public void FinishCheckFlag()
    {
        stageManager.monsterCount--;
        if (stageManager.monsterCount.Equals(0))
        {
            stageManager.stageLength--;
            if (stageManager.stageLength.Equals(0))
            {
                stageManager.isClear = true;
                stageManager.StageFinish();
            }
        }
    }
}
