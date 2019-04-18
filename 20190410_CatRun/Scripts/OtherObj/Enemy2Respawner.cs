using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Respawner : MonoBehaviour {

    public float[] coolTime;            //쿨타임 시간을 정렬해봄.
    public float respawnTime = 2.0f;     //리스폰 시간

    public float enemyRespawnPosition = 13.0f; // 적이 리스폰 되는 위치

                                               // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
