using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MORPG_CharacterStats : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth
    {
        get;
        private set;
    }
    public float maxDefence;
    public float currentDefence
    {
        get;
        private set;
    }

    public float damage
    {
        get;
        private set;
    }

    // TODO (장현명) : 수치 넣어야 한다.
   
    private void Start()
    {
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + "die");
    }
}
