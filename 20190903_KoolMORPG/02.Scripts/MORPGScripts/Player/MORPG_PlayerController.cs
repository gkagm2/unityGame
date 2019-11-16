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
    private Animator anim;
    private MORPG_CharacterCombat combat;
    //private MORPG_CharacterStats stats;
    private MORPG_CharacterStats stats;

    [Header("LayerMask")]
    public LayerMask movementMask;
    public LayerMask monsterMask;
    public LayerMask npcMask;


    void Start()
    {
        motor = GetComponent<MORPG_PlayerMotor>();
        anim = GetComponent<Animator>();
        combat = GetComponent<MORPG_CharacterCombat>();
        stats = GetComponent<MORPG_CharacterStats>();


        // 플레이어 스텟 설정하기
        stats.damage = PlayerInformation.userData.TotalAtk;
        stats.maxDefence = stats.currentDefence = PlayerInformation.userData.def;
        stats.maxHealth = stats.currentHealth = PlayerInformation.userData.hp;
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
                    //Debug.Log("Ground");
                    mouseClickParticle.UseObject(hit.point);
                    motor.MoveToPoint(hit.point);
                    RemoveFocus();
                }
                if (hit.transform.CompareTag("MORPG_Monster"))
                {
                    motor.MoveToPoint(hit.point);

                    MORPG_Interactable interactable = hit.collider.GetComponent<MORPG_Interactable>();
                    MORPG_EnemyController enemyController = hit.collider.GetComponent<MORPG_EnemyController>();
                    MORPG_CharacterStats enemyStats = hit.collider.GetComponent<MORPG_CharacterStats>();
                    if (interactable != null)
                    {
                        SetFocus(interactable);
                        combat.Attack(enemyStats, stats.damage);
                    }

                    else
                    {
                        RemoveFocus();
                    }
                    Debug.Log("Monster");
                }
            }


            // LayerMask를 이용한 코드

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