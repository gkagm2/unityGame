using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public EnemyBasicComponent ebc;
    public PlayerMovement pm;

    void Start()
    {
        ebc = GetComponentInParent<EnemyBasicComponent>();
        pm = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Damage();
        }
    }
    
    public void Damage()
    {
        pm.hp -= ebc.atk - (pm.def * 0.5f);
    }

}
