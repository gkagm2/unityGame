using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MORPG_EnemyController : MORPG_Interactable
{
    [Tooltip("순찰 쿨타임 범위")]
    public float patrolMinCoolTime = 4.0f;
    public float patrolMaxCoolTime = 5.0f;
    private float patrolCoolTime = 0f;

    public float lookRadius = 10f;
    public float patrolPositionChangeTime = 3.0f;
    public float patrolArea;
    private float _patrolArea;

    private Vector3 respawnPosition;
    private Transform target;
    private NavMeshAgent agent;
    private MORPG_CharacterCombat combat;
    private MORPG_CharacterStats stats;
    private Animator anim;

    private enum EState
    {
        patrol,
        attack
    }
    private EState eState = EState.patrol;

    private void Start()
    {
        target = GameObject.FindWithTag("MORPG_Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<MORPG_CharacterCombat>();

        stats = GetComponent<MORPG_CharacterStats>();

        anim = GetComponentInChildren<Animator>();
        respawnPosition = transform.position;
    }


    private void Update()
    {
        float distance;

        if (target != null)
        {
            distance = Vector3.Distance(target.position, transform.position);
        }
        else
        {
            distance = lookRadius + 1f; //범위보다 크게 설정해서 순찰할 수 있게 한다.
        }

        if (distance <= lookRadius)
        {
            eState = EState.attack;
        }
        else
        {
            eState = EState.patrol;
        }
        switch (eState)
        {
            case EState.patrol:
                Patrol();
                break;
            case EState.attack:
                agent.SetDestination(target.position);

                if (distance <= agent.stoppingDistance) // 거리 안으로 들어오면
                {
                    MORPG_CharacterStats targetStats = target.GetComponent<MORPG_CharacterStats>();
                    combat.Attack(targetStats, stats.damage);
                }
                break;
        }
    }

    /// <summary>
    /// 순찰한다.
    /// </summary>
    public void Patrol()
    {
        patrolCoolTime += Time.deltaTime;

        if (patrolCoolTime >= Random.Range(patrolMinCoolTime, patrolMaxCoolTime + 1))
        {
            float _patrolAreaX = Random.Range(-patrolArea, patrolArea);
            float _patrolAreaY = Random.Range(-patrolArea, patrolArea);

            Vector3 patrolPosition = new Vector3(respawnPosition.x + _patrolAreaX, respawnPosition.y, respawnPosition.z + _patrolAreaY);
            agent.SetDestination(patrolPosition);

            patrolCoolTime = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(respawnPosition, patrolArea);
    }

}
