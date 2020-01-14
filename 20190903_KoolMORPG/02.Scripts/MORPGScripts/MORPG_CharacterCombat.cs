using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MORPG_CharacterStats))]
public class MORPG_CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    public float attackCooldown;
    public float attackDelay = 1f;
    public GameObject attackEffect;

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

    /// <summary>
    /// 공격한다
    /// </summary>
    /// <param name="targetStats">타겟 캐릭터 상태</param>
    /// <param name="damage">데미지 값</param>
    public void Attack(MORPG_CharacterStats targetStats, float damage)
    {
        if(attackCooldown <= 0)
        {
            StartCoroutine(IDoDamage(targetStats, damage, attackDelay));

            attackCooldown = 1f / attackSpeed;
        }
    }

    private IEnumerator IDoDamage(MORPG_CharacterStats stats, float damage, float delay)
    {
        yield return new WaitForSeconds(delay);
        // TODO (장현명) : 이쪽이 문제
        Instantiate(attackEffect, transform.position, Quaternion.identity);
        stats.TakeDamage(damage);
    }
}
