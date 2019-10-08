using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public Vector3 moveDir; //이동방향
    public Vector3 avoidanceDir; //회피 방향
    public List<GameObject> target;

    public float moveSpeed = 5f; //이동속도
    public float avoidanceSpeed = 10f; //회피속도
    public float avoidanceTime = 0.5f; //회피시간
    public float attackSpeed; //공격속도
    public float meleeAttackRange = 2.5f;
    public float damagedTime = 0.2f;
    public float hp;
    public float atk;
    public float def;


    public int level;

    public ParticleSystem hpPotionParticle;

    public UserData userData;

    private StageManager stageManager;
    private Transform tr;
    private CharacterController cc;
    public Material material;

    public void Initialize()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        tr = GetComponent<Transform>();
        cc = GetComponent<CharacterController>();
        material = GetComponentInChildren<SkinnedMeshRenderer>().material;
    } 

    /// <summary>
    /// 캐릭터 움직임
    /// </summary>
    public void Movement() 
    { 
        cc.SimpleMove(tr.forward * moveSpeed);
    }

    /// <summary>
    /// 캐릭터 회전
    /// </summary>
    public void PlayerRotate() 
    {
        if (moveDir.Equals(Vector3.zero)) return;
        tr.rotation = Quaternion.LookRotation(moveDir);
    }

    /// <summary>
    /// 가상 조이스틱 방향 값 가져오기
    /// </summary>
    public void SetDir()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        moveDir = new Vector3(h, 0, v);
    } 
    
    /// <summary>
    /// 회피기
    /// </summary>
    public void Avoidance()
    {
        cc.SimpleMove(tr.forward * avoidanceSpeed);
    }

    /// <summary>
    /// 가장 가까운 몬스터 자동 조준
    /// </summary>
    public void LockOn()
    {
        GameObject enemy = GetNearestTargetGameObject();
        if(enemy)
        {
            tr.rotation = Quaternion.Lerp(tr.rotation, Quaternion.LookRotation(new Vector3(enemy.transform.position.x, 0, enemy.transform.position.z) - new Vector3(tr.position.x, 0, tr.position.z)), 5f * Time.deltaTime);
        }
    }

    /// <summary>
    /// 가장 가까운 몬스터 정보 갖고오기
    /// </summary>
    /// <returns></returns>
    public GameObject GetNearestTargetGameObject()
    {
        // 아무것도 없으면 리턴
        if (target.Count == 0)
            return null; 

        int index = 0;
        float minDistance = Vector3.Distance(tr.position, target[0].transform.position);
        for(int i=1; i < target.Count; i++)
        {
            if (minDistance > Vector3.Distance(tr.position, target[i].transform.position))
            {
                minDistance = Vector3.Distance(tr.position, target[i].transform.position);
                index = i;
            }
        }
        return target[index];
    }

    /// <summary>
    /// 가장 가까운 몬스터에게 돌진
    /// </summary>
    public void EnemyChasing()
    {
        if (target.Count == 0) return;
        GameObject enemy = GetNearestTargetGameObject();

        if (Vector3.Distance(tr.position, enemy.transform.position) >= meleeAttackRange)
        {
            tr.rotation = Quaternion.LookRotation(new Vector3(enemy.transform.position.x,0,enemy.transform.position.z) - new Vector3(tr.position.x, 0, tr.position.z));
            cc.SimpleMove(tr.forward * avoidanceSpeed);
            if(Vector3.Distance(new Vector3(tr.position.x, 0, tr.position.z), new Vector3(enemy.transform.position.x, 0, enemy.transform.position.z)) <= meleeAttackRange)
            {
                return;
            }
        }
    }

    public void PlayerDeath()
    {
        stageManager.isClear = false;
        
        stageManager.StageFinish();
    }

    public void UseHpPotionParticle()
    {
        hpPotionParticle.Play();
    }
}
