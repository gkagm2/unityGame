using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MORPG_CharacterStats : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    public float maxDefence = 100f;
    public float currentDefence = 100f;

    public float damage = 10;

    /// <summary>
    /// 데미지를 받는다.
    /// </summary>
    /// <param name="damage">데미지 값</param>
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// 죽는다
    /// </summary>
    public virtual void Die()
    {
        Debug.Log(transform.name + "die");

        // 임시로 삭제한다. 나중에 메모리풀로 만들어서 하기
        Destroy(gameObject);
    }
}
