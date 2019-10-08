using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public StageManager stageManager;

    private void Start()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
    }
}
