using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(MORPG_PlayerMotor))]
public class MORPG_PlayerController : MonoBehaviour
{
    
    // Objects
    public Camera cam; // cam : camera
    public ObjectPool mouseClickParticle;
    private MORPG_PlayerMotor motor;
    private MORPG_Interactable focus;

    [Header("LayerMask")]
    public LayerMask movementMask;
    public LayerMask monsterMask;
    public LayerMask npcMask;


    void Start()
    {
        motor = GetComponent<MORPG_PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // Click screen 
        if (Input.GetButtonDown("MORPG_Fire1"))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Click ground
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("MORPG_Npc"))
                {
                    Debug.Log("Npc");
                    MORPG_Interactable interactable = hit.collider.GetComponent<MORPG_Interactable>();

                    if (interactable != null)
                    {
                        Debug.Log("interactable은 null이 아니다");
                        SetFocus(interactable);
                    }
                    else
                    {
                        Debug.Log("interactable은 null이다.?");
                        RemoveFocus();
                    }

                }
                if (hit.transform.CompareTag("MORPG_Ground"))
                {
                    Debug.Log("Ground");
                    mouseClickParticle.UseObject(hit.point);
                    motor.MoveToPoint(hit.point);
                    RemoveFocus();
                }
                if (hit.transform.CompareTag("MORPG_Monster"))
                {
                    Debug.Log("Monster");
                }
            }

            //// Click ground
            //if (Physics.Raycast(ray, out hit, movementMask))
            //{
            //    //Debug.Log("movementMask : " + movementMask.value);
            //    mouseClickParticle.UseObject(hit.point);
            //    motor.MoveToPoint(hit.point);
            //    RemoveFocus();
            //}

            //// Click monster
            //if(Physics.Raycast(ray,out hit, monsterMask))
            //{
            //    //Debug.Log("monsterMask : " + monsterMask.value);
            //}
            //// Click npc
            //if (Physics.Raycast(ray, out hit, npcMask))
            //{
            //    //Debug.Log("npcMask : " + npcMask.value);
            //    MORPG_Interactable interactable = hit.collider.GetComponent<MORPG_Interactable>();
                    
            //    if (interactable != null)
            //    {
            //        Debug.Log("interactable은 null이 아니다");
            //        SetFocus(interactable);
            //    } else
            //    {
            //        Debug.Log("interactable은 null이다.?");
            //        RemoveFocus();
            //    }
            //}
        }
    }

    private void SetFocus(MORPG_Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if(focus != null)
            {
                focus.OnDefocused();
                focus = newFocus;
                motor.FollowTarget(newFocus);
            }
        }

        focus = newFocus;
        newFocus.OnFocused(transform);
        motor.FollowTarget(newFocus);
    }

    private void RemoveFocus()
    {
        if(focus != null)
        {
            Debug.Log("focus는 null이 아니다.");
            focus.OnDefocused();
        }
        focus = null;
        motor.StopFollowingTarget();
    }
}