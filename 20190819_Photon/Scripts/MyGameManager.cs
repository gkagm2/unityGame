using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour {

    private PhotonView pv;
    public Transform[] spawnPoint;
    public float createTime = 3.0f;

	// Use this for initialization
	void Start () {
        pv = PhotonView.Get(this);

        spawnPoint = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		if(PhotonNetwork.connected && PhotonNetwork.isMasterClient)
        {
            if(Time.time > createTime)
            {
                MakeEnemy();
                createTime = Time.time + 3.0f;
            }
        }
	}

    private void MakeEnemy()
    {
        StartCoroutine(this.CreateEnemy());
    }

    IEnumerator CreateEnemy()
    {
        int idx = UnityEngine.Random.Range(1, spawnPoint.Length);
        PhotonNetwork.InstantiateSceneObject("Enemy", spawnPoint[idx].position, Quaternion.identity, 0, null);
        yield return null;
    }
}
