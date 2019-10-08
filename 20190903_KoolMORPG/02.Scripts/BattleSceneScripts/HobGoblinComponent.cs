using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HobGoblinComponent : EnemyBasicComponent
{
    private Animator am;

    private void Awake()
    {
        am = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (isDeath) return;
        DistanceCheck();
        EnemyRotate();
        DamagedProcess();
        switch (eEnemyState)
        {
            case EEnemyState.idle:
                FindPlayer(findDis);
                if (isMove)
                {
                    am.SetBool("isMove", isMove);
                    eEnemyState = EEnemyState.move;
                }
                break;
            case EEnemyState.move:
                MoveToPlayer(attackableDis);
                if (isAttack)
                {
                    isMove = false;
                    am.SetBool("isAttack", isAttack);
                    eEnemyState = EEnemyState.attack;
                }
                break;
            case EEnemyState.damage:
                break;
            case EEnemyState.attack:
                if (isMove)
                {
                    isAttack = false;
                    am.SetBool("isAttack", isAttack);
                    eEnemyState = EEnemyState.move;
                }
                ChasingPlayer(attackableDis);
                break;
            case EEnemyState.death:
                
                break;
            default:
                break;
        }
    }

    private void OnDisable()
    {
        eEnemyState = EEnemyState.idle;
        MonsterRecycle();
    }

    public IEnumerator Damaged()
    {
        isDamaged = false;
        nma.isStopped = true;
        yield return new WaitForSeconds(am.GetCurrentAnimatorStateInfo(0).length);
        nma.isStopped = false;
        eEnemyState = EEnemyState.idle;
    }

    public IEnumerator Dead()
    {
        yield return new WaitForSeconds(deathTime);
        isDeath = false;
        gameObject.SetActive(false);
    }

    public void DamagedProcess()
    {
        if (isDamaged)
        {
            am.SetBool("isAttack", false);
            am.SetBool("isMove", false);
            am.SetTrigger("isDamaged");
            StartCoroutine(Damaged());
        }
    }

    public void WeaponCollOnOff()
    {
        StartCoroutine(AttackTime());
    }

    public IEnumerator AttackTime()
    {
        enemyWeapon.enabled = true;
        yield return new WaitForSeconds(collOnTime);
        enemyWeapon.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDeath) return;
        if (other.CompareTag("PlayerWeapon"))
        {
            SoundEffectOn();
            ps.Play();
            isDamaged = true;
            eEnemyState = EEnemyState.damage;
            DamagedCalculation();
            if (hp <= 0)
            {
                DeadProcess();
                eEnemyState = EEnemyState.death;
                nma.isStopped = true;
                pm.target.Remove(gameObject);
                isDeath = true;
            }
        }
    }

    public void DeadProcess()
    {
        am.SetTrigger("isDeath");
        StartCoroutine(Dead());
        eEnemyState = EEnemyState.death;
        FinishCheckFlag();
    }

    public void SoundEffectOn()
    {
        if (PlayerPrefs.GetInt("SoundEffectCheck") == 0) return;
        damagedSound.Play();
    }
}
