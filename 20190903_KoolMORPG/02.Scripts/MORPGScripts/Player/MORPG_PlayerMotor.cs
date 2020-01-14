using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class MORPG_PlayerMotor : MonoBehaviour
{
    private Transform targetTransform;
    private NavMeshAgent agent;
    private GameObject characterModelObject;
    private Animator anim;
    private int isMoveHash = Animator.StringToHash("isMove");

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // character type 0 : warrior, 1 : archer
        switch (PlayerInformation.userData.characterType)
        {
            case EPlayerCharacterType.warrior:
                characterModelObject = Resources.Load<GameObject>(PathOfResources.morpg_PlayerWarrior);
                break;
            case EPlayerCharacterType.archer:
                characterModelObject = Resources.Load<GameObject>(PathOfResources.morpg_PlayerArcher);
                break;
            default:
                Debug.LogWarning("<DEBUG MODE> 플레이어 타입이 선택되지 않았습니다.");
                characterModelObject = Resources.Load<GameObject>(PathOfResources.morpg_PlayerWarrior);
                break;
        }
        GameObject characterModel = Instantiate(characterModelObject, transform.position, transform.rotation, transform) as GameObject;
        if (characterModel)
        {
            Debug.Log("찾았다능~");
            anim = GetComponentInChildren<Animator>();
        }
        else
        {
            Debug.Assert(false, "캐릭터를 가져오지 못했습니다.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (targetTransform != null)
        {
            Debug.Log("move");
            agent.SetDestination(targetTransform.position);
            LookAtTarget();
        }

        // animtion
        if (agent.remainingDistance <= agent.stoppingDistance + 0.2f)
        {
            anim.SetBool(isMoveHash, false);
        }
        else
        {
            anim.SetBool(isMoveHash, true);
        }   
    }

    /// <summary>
    /// 목표 위치로 움직인다.
    /// </summary>
    /// <param name="point">목표 위치</param>
    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    /// <summary>
    /// 타겟을 따라다닌다.
    /// </summary>
    /// <param name="newTarget">새로운 타겟</param>
    public void FollowTarget(MORPG_Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.radius * 0.8f;
        agent.updateRotation = false;
        targetTransform = newTarget.interactionTransform;
    }

    /// <summary>
    /// 타겟을 따라다니는 것을 멈춘다.
    /// </summary>
    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
        targetTransform = null;
        //Debug.Log("agent.updatePosition!");
        agent.updateRotation = true;
    }

    /// <summary>
    /// Character Face look at the target
    /// </summary>
    public void LookAtTarget()
    {
        //Debug.Log("targetTransform.position : " + targetTransform.position);
        Vector3 direction = (targetTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }
}
