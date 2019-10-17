using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class MORPG_PlayerMotor : MonoBehaviour
{
    private Transform targetTransform;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(targetTransform != null)
        {
            agent.SetDestination(targetTransform.position);
            LookAtTarget();
        }
    }
    
    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void FollowTarget(MORPG_Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.radius * 0.8f;
        agent.updateRotation = false;
        targetTransform = newTarget.interactionTransform;
    }

    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
        targetTransform = null;
        Debug.Log("agent.updatePosition!");
        agent.updateRotation = true;
    }

    /// <summary>
    /// Character Face look at the target
    /// </summary>
    public void LookAtTarget()
    {
        Debug.Log("targetTransform.position : " + targetTransform.position);
        Vector3 direction = (targetTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }
}
