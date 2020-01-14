using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MORPG_Interactable : MonoBehaviour
{
    public float radius = 2f;
    public Transform interactionTransform;

    private bool isFocus = false;
    private Transform player;
    private bool hasInteracted = false;

    public virtual void Interact()
    {
        Debug.Log("Interacting with " + transform.name);
    }

    private void Update()
    {
        if(isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if(distance <= radius)
            {
                Debug.Log("INTERACT");
                Interact();
                hasInteracted = true;

            }
        }
    }

    /// <summary>
    /// 플레이어의 위치쪽으로 포커스한다.
    /// </summary>
    /// <param name="playerTransform">플레이어의 Transform</param>
    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    /// <summary>
    /// 포커스가 풀린다.
    /// </summary>
    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {
        if(interactionTransform == null)
        {
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
