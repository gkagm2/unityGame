using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MRPG_CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    public float attackDelay = 0.6f;

    private float attackCooldown = 0f;

    public event System.Action OnAttack;

    MORPG_CharacterStats myStats;

    private Animator anim;
    public int isAttackHash = Animator.StringToHash("isAttack");

    // Start is called before the first frame update
    void Start()
    {
        myStats = GetComponent<MORPG_CharacterStats>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

    public void Attack(MORPG_CharacterStats targetStats)
    {
        if(attackCooldown <= 0)
        {
            StartCoroutine(IDoDamage(targetStats, attackDelay));
            anim.SetBool(isAttackHash, true);
            if (OnAttack != null)   
            {
                OnAttack();
            }
            attackCooldown = 1.0f / attackSpeed;
        }
    }

    IEnumerator IDoDamage(MORPG_CharacterStats stats, float attackDelay)
    {
        yield return new WaitForSeconds(attackDelay);
        Debug.Log(stats.gameObject.name + "에게 Damage를 준다.");
        //stats.TakeDamage(myStats.damage)
    }
}
