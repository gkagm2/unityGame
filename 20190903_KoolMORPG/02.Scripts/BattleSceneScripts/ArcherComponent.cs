using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherComponent : PlayerMovement
{
    public GameObject[] arrowArray;
    public GameObject arrow;
    public Transform[] arrowPoint;
    public HUDManager hm;
    public GameObject dashTrail;
    public AudioSource bowSound;

    public int arrowCnt = 20;
    public int arrowIdx = 0;

    public bool isAttack = false;       
    public bool isLockOn;

    public enum EArcherState
    {
        idle,
        move,
        dash,
        attack,
        skill1,
        skill2,
        damaged,
        death
    }

    public EArcherState ePlayerState = EArcherState.idle;

    private Animator am;

    void Awake()
    {

    }
    void Start()
    {
        am = GetComponent<Animator>();
        Initialize();
        arrowPoint = GameObject.Find("Arrowpoint").GetComponentsInChildren<Transform>();
        
        hm = GameObject.Find("HUDManager").GetComponent<HUDManager>();
        ArrowArrayInit();
        PlayerStatusInitialize();
    }

    public void PlayerStatusInitialize()
    {
        //PlayerInformation.userData.SetCharacterStat(EPlayerCharacterType.archer, 1);

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
        switch (ePlayerState)
        {
            case EArcherState.idle:
                if (moveDir!=Vector3.zero)
                {
                    am.SetBool("isMove", true);
                    ePlayerState = EArcherState.move;
                }

                if (Input.GetButtonDown("Dash"))
                {
                    am.SetTrigger("Dash");
                    StartCoroutine(IAvoidanceEnd());
                    ePlayerState = EArcherState.dash;
                }

                if(Input.GetButtonDown("BasicAttack"))
                {
                    BasicAttack();
                }

                if(Input.GetButtonDown("Skill1"))
                {
                    Skill1();
                }

                if (Input.GetButtonDown("Skill2"))
                {
                    PressSkill2Btn();
                }

                break;
            case EArcherState.move:

                Movement();
                PlayerRotate();
                if (moveDir.Equals(Vector3.zero))
                {
                    ePlayerState = EArcherState.idle;
                    am.SetBool("isMove", false);
                }

                if(Input.GetButtonDown("Dash"))
                {
                    am.SetTrigger("Dash");
                    
                    avoidanceDir = moveDir;
                    StartCoroutine(IAvoidanceEnd());
                    ePlayerState = EArcherState.dash;
                }

                if (Input.GetButtonDown("BasicAttack"))
                {
                    BasicAttack();
                    am.SetBool("isMove", false);
                }

                if (Input.GetButtonDown("Skill1"))
                {
                    Skill1();
                    am.SetBool("isMove", false);
                }

                if (Input.GetButtonDown("Skill2"))
                {
                    am.SetBool("isMove", false);
                    PressSkill2Btn();
                }

                break;
            case EArcherState.dash:
                Avoidance();
                
                break;
            case EArcherState.attack:
                PlayerRotate();
                LockOn();
                if(Input.GetButton("BasicAttack"))
                {
                    isAttack = true;
                }
                else
                {
                    isAttack = false;
                }
                break;
            case EArcherState.skill1:
                LockOn();
                break;
            case EArcherState.skill2:
                break;
            case EArcherState.damaged:
                break;
            case EArcherState.death:
                break;
            default:
                break;
        }
    }

    public IEnumerator IAvoidanceEnd()
    {
        dashTrail.SetActive(true);
        yield return new WaitForSeconds(avoidanceTime);
        dashTrail.SetActive(false);
        if(moveDir != Vector3.zero)
        {
            am.SetBool("isMove", true);
            ePlayerState = EArcherState.move;
        }
        else
        {
            am.SetBool("isMove", false);
            ePlayerState = EArcherState.idle;
        }
    }

    public void BasicAttack()
    {
        am.SetBool("BasicAttack", true);
        ePlayerState = EArcherState.attack;
    }

    public void Skill1()
    {
        am.SetBool("3ComboAttack", true);
        ePlayerState = EArcherState.skill1;
    }

    public void Skill1End()
    {
        am.SetBool("3ComboAttack", false);
        ePlayerState = EArcherState.idle;
    }

    public void BasicAttackEnd()
    {
        if (isAttack) return;
        am.SetBool("BasicAttack", false);
        ePlayerState = EArcherState.idle;
    }

    public void ArrowInit()
    {
        ArrayArrowFire();
    }

    public void ArrayArrowFire()
    {
        if (arrowIdx.Equals(arrowArray.Length)) arrowIdx = 0;
        arrowArray[arrowIdx].transform.position = arrowPoint[0].position;
        arrowArray[arrowIdx].transform.rotation = arrowPoint[0].rotation;
        arrowArray[arrowIdx].SetActive(true);
        arrowIdx++;
    }

    public void ArrowCaseInit()
    {
        for (int i = 0; i < arrowCnt; i++)
        {
            GameObject obj = Instantiate(arrow) as GameObject;
            obj.transform.name = "Arrow " + i;
        }
    }

    public void Skill2()
    {
        for (int i = 0; i < arrowPoint.Length; i++)
        {
            if (arrowIdx.Equals(arrowArray.Length)) arrowIdx = 0;
            arrowArray[arrowIdx].transform.position = arrowPoint[i].position;
            arrowArray[arrowIdx].transform.rotation = arrowPoint[i].rotation;
            arrowArray[arrowIdx].SetActive(true);
            arrowIdx++;
        }
    }

    public void ArrowArrayInit()
    {
        arrowArray = new GameObject[arrowCnt];

        for (int i = 0; i < arrowCnt; i++)
        {
            GameObject arw = Instantiate(arrow) as GameObject;
            arrowArray[i] = arw;
            arw.transform.name = "AArrow " + i;
        }
    }

    public void PressSkill2Btn()
    {
        isLockOn = true;
        am.SetBool("Ultimate", true);
        arrowPoint[0].localRotation = Quaternion.Euler(-35, 0, 0); // TODO : 숫자를 변수로 바꾸기
        ePlayerState = EArcherState.skill2;
    }

    public void Skill2End()
    {
        isLockOn = false;
        am.SetBool("Ultimate", false);
        arrowPoint[0].localRotation = Quaternion.Euler(Vector3.zero);
        ePlayerState = EArcherState.idle;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ePlayerState == EArcherState.death) return;
        if (other.CompareTag("EnemyWeapon"))
        {
            hm.PlayerHpUpdate();
            StartCoroutine(DamagedEffect());
            if (hp <= 0)
            {
                ePlayerState = EArcherState.death;
                am.SetTrigger("Death");
                PlayerDeath();
            }
        }
    }

    public IEnumerator DamagedEffect()
    {
        material.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(damagedTime);
        material.SetColor("_Color", Color.white);
    }
    public void BowSound()
    {
        bowSound.Play();
    }
}
