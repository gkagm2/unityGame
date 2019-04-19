using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner {
    PlayerControl playerControl;

    void Start()
    {
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
    }
    
    public void RandomPos(ref Transform transform)
    {

        Debug.Log("Respawner playerControl moveLimit " + playerControl.moveLimit);
        transform.position = new Vector3(Random.Range(-playerControl.moveLimit, playerControl.moveLimit), 0, 40);
    }
}
