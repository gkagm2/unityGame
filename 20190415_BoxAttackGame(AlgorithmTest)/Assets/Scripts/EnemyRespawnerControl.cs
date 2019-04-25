using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnerControl : MonoBehaviour {

    //respawn select
    public short selectRespawnType;


    //respawn cool time
    public float coolTime;
    float tempTime;

    public float randomXPosition;

    public GameObject enemyObject;
	// Use this for initialization
	void Start () {
        tempTime = coolTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameManager.isGameOver)
        {
            RespawnEnemy();
           
        }
	}

    public void RespawnEnemy()
    {
        tempTime -= Time.deltaTime;
        //Debug.Log("tempTime : " + tempTime);
        if(tempTime < 0)
        {
            randomXPosition = GetRandomXPosition();
            transform.position = new Vector3(randomXPosition, transform.position.y, transform.position.z);

            Instantiate(enemyObject, transform.position, transform.rotation);
            tempTime = coolTime;
        }
    }
    public float GetRandomXPosition()
    {
        return Random.Range(-randomXPosition, randomXPosition);
    }
}
