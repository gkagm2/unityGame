using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInitTrigger : MonoBehaviour
{
    public StageManager stageManager;

    private SphereCollider sphereCollider;

    private void Start()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            stageManager.MonsterEnable();
            sphereCollider.enabled = false;
            if (stageManager.areaIdx < stageManager.area.Length)
            {
                //stageManager.areaIdx++;
                stageManager.areaIdx++;
                stageManager.NextArea();
            }
        }
       
    }
}
