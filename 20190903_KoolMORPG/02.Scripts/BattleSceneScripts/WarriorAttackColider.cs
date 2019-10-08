using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAttackColider : MonoBehaviour
{
    public CameraPosition cp;

    private void Awake()
    {
        cp = GameObject.FindWithTag("MainCamera").GetComponent<CameraPosition>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            cp.isShaking = true;
        }
    }
}
