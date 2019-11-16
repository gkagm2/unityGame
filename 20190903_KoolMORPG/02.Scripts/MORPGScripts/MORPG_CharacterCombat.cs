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

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

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
        stats.TakeDamage(damage);
    }
}
