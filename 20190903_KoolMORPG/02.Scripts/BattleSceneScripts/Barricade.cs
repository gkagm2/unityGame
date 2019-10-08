using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    private Animator am;
    private BoxCollider bc;
    private StageManager sm;

    void Start()
    {
        am = GetComponent<Animator>();
        bc = GetComponent<BoxCollider>();
        sm = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(sm.monsterCount == 0)
        {
            if(other.CompareTag("Player"))
            {
                am.SetTrigger("isTouched");
                bc.enabled = false;
            }
        }
    }
}
