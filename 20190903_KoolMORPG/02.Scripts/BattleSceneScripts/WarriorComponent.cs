using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeoLuz.PlugAndPlayJoystick;

public class WarriorComponent : PlayerMovement
{
    public bool isAttack = false;
    public SphereCollider attackCollider;
    public HUDManager hm;
    public GameObject[] trail;
    public AudioSource[] attackSound;
    public AudioSource attackGround;
    public GameObject dashTrail;
    public ParticleSystem ps;
    public ParticleSystem buffPs;
    public float buffTime = 10.0f;

    public int soundCount = 0;
    public bool isBuff = false;

    /// <summary>
    /// Warrior Finite State Machine
    /// </summary>
    public enum EWarriorState
    {
        idle,
        move,
        dash,
        attack,
        attack2,
        attack3,
        skill1,
        skill2,
        damaged,
        death
    }

    public EWarriorState eWarriorState = EWarriorState.idle;

    private Animator am;
    
    void Start()
    {
        am = GetComponent<Animator>();
        hm = GameObject.Find("HUDManager").GetComponent<HUDManager>();
        Initialize();
        attackCollider = GameObject.Find("AttackPoint").GetComponent<SphereCollider>();
        attackCollider.enabled = false;
        PlayerStatusInitialize();
    }

    public void PlayerStatusInitialize()
    {
        //PlayerInformation.userData.SetCharacterStat(EPlayerCharacterType.warrior, 1);

        userData = PlayerInformation.userData.GetInGameCharacterData();
        hp = userData.hp;
        def = userData.TotalDef;
        atk = userData.TotalAtk;
        level = userData.level;
        hm.GetPlayerMaxHp();
    }

    void Update()
    {
        SetDir();
        switch (eWarriorState)
        {
            case EWarriorState.idle:
                if(moveDir!=Vector3.zero)
                {
                    am.SetBool("isMove", true);
                    eWarriorState = EWarriorState.move;
                }

                if(Input.GetButtonDown("Dash"))
                {
                    StartCoroutine(AvoidanceTime());
                }

                BasicAttack();
                Skill();
                break;
            case EWarriorState.move:
                Movement();
                PlayerRotate();
                Skill();
                if(moveDir.Equals(Vector3.zero))
                {
                    am.SetBool("isMove", false);
                    eWarriorState = EWarriorState.idle;
                }

                if (Input.GetButtonDown("Dash"))
                {
                    StartCoroutine(AvoidanceTime());
                }

                BasicAttack();

                break;
            case EWarriorState.dash:
                Avoidance();
                break;
            case EWarriorState.attack:
                EnemyChasing();
                //LockOn();
                ComboAttack();
                break;
            case EWarriorState.attack2:
                EnemyChasing();
                LockOn();
                ComboAttack();
                break;
            case EWarriorState.attack3:
                break;
            case EWarriorState.skill1:
                break;
            case EWarriorState.skill2:
                LockOn();
                break;
            case EWarriorState.damaged:
                break;
            case EWarriorState.death:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 기본 공격 애니메이션 & fsm 변환 함수
    /// </summary>
    public void BasicAttack()
    {
        if(Input.GetButtonDown("BasicAttack"))
        {
            am.SetBool("isMove", isAttack);
            isAttack = true;
            am.SetBool("isAttack", isAttack);
            eWarriorState = EWarriorState.attack;
            for (int i = 0; i < trail.Length; i++)
            {
                trail[i].SetActive(true);
            }
        }
    }

    public void ComboAttack()
    {
        if(Input.GetButtonDown("BasicAttack"))
        {
            isAttack = true;
            am.SetBool("isAttack", isAttack);
            eWarriorState = EWarriorState.attack2;
            for (int i = 0; i < trail.Length; i++)
            {
                trail[i].SetActive(true);
            }

        }
    }

    public void AttackAnimStart()
    {
        for (int i = 0; i < trail.Length; i++)
        {
            trail[i].SetActive(true);
        }
        isAttack = false;
        am.SetBool("isAttack", isAttack);
    }

    public void AttackAnimEnd()
    {

        if (!isAttack)
        {
            am.SetBool("isAttack", isAttack);
            eWarriorState = EWarriorState.idle;
            for (int i = 0; i < trail.Length; i++)
            {
                trail[i].SetActive(false);
            }
        }
        else
        {
            return;
        }
    }

    public void ComboEnd()
    {
        isAttack = false;
        am.SetBool("isAttack", isAttack);
        eWarriorState = EWarriorState.idle;
        for (int i = 0; i < trail.Length; i++)
        {
            trail[i].SetActive(false);
        }
    }

    public void SkillEnd()
    {
        for (int i = 0; i < trail.Length; i++)
        {
            trail[i].SetActive(false);
        }
        eWarriorState = EWarriorState.idle;
    }

    // 회피기 Complete
    public IEnumerator AvoidanceTime()
    {
        dashTrail.SetActive(true);
        am.SetTrigger("Dash");
        avoidanceDir = moveDir;
        eWarriorState = EWarriorState.dash;
        yield return new WaitForSeconds(am.GetCurrentAnimatorStateInfo(0).length);
        dashTrail.SetActive(false);
        if (moveDir != Vector3.zero)
        {
            am.SetBool("isMove", true);
            eWarriorState = EWarriorState.move;
        }
        else
        {
            am.SetBool("isMove", false);
            eWarriorState = EWarriorState.idle;
        }
    }

    public void Skill()
    {
        if(Input.GetButtonDown("Skill1"))
        {
            BuffOnOff();
        }

        if (Input.GetButtonDown("Skill2"))
        {
            eWarriorState = EWarriorState.skill2;
            for (int i = 0; i < trail.Length; i++)
            {
                trail[i].SetActive(true);
            }
            am.SetBool("isMove", false);
            am.SetTrigger("Skill2");
        }
    }

    public void AttackColliderOnOff()
    {
        if(attackCollider.enabled)
        {
            attackCollider.enabled = false;
        }
        else
        {
            attackCollider.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (eWarriorState == EWarriorState.death) return;
        if (other.CompareTag("EnemyWeapon"))
        {
            hm.PlayerHpUpdate();
            StartCoroutine(DamagedEffect());
            if (hp <= 0)
            {
                Debug.Log("죽음");
                eWarriorState = EWarriorState.death;
                am.SetTrigger("Death");
                PlayerDeath();
            }
        }
    }

    public IEnumerator DamagedEffect()
    {
        material.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(damagedTime);
        if(isBuff)
        {
            material.SetColor("_Color", Color.yellow);
        }
        else
        {
            material.SetColor("_Color", Color.white);
        }
        
    }

    public void SwordSpinParticle()
    {
        ps.Play();
    }

    public void BuffOnOff()
    {
        if (isBuff) return;
        eWarriorState = EWarriorState.skill1;
        isBuff = true;
        am.SetBool("isMove", false);
        am.SetTrigger("Skill1");
        StartCoroutine(BuffSkill());
    }

    public IEnumerator BuffSkill()
    {
        float tempAtk = atk * 0.5f;
        float tempDef = def * 0.5f;
        atk += tempAtk;
        def += tempDef;
        buffPs.Play();
        material.SetColor("_Color", Color.yellow);
        yield return new WaitForSeconds(buffTime);
        atk -= tempAtk;
        def -= tempDef;
        buffPs.Stop();
        material.SetColor("_Color", Color.white);
        isBuff = false;
    }

    public void AttackSound()
    {
        attackSound[soundCount].Play();
        soundCount++;
        if (soundCount == 3) soundCount = 0;
    }

    public void AttackGround()
    {
        attackGround.Play();
    }
}
