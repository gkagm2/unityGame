using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideEnemyRespawner : MonoBehaviour {
    public bool direction;
    public float respawnPositionX;
    public GameObject sideEnemys;

    public float coolTime;
    float tempTime;

    public float coolTime2;
    float tempTime2;

	// Use this for initialization
	void Start () {
        tempTime = coolTime;
        tempTime2 = coolTime2;
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameManager.isGameOver)
        {
            SetRespawnPosition(); ///리스폰할 곳을 랜덤으로 지정해줌
            RespawnEnemy();
        }
    }
    //TODO : 뭔가 이 코드가 안먹히는 것 같음.
    public void SetRespawnPosition()
    {
        tempTime2 = Time.deltaTime;
        if (tempTime2 < 0)
        {
            direction = GetRandomDirection();
            Debug.Log("direction : " + direction);
            if (direction) // true면 왼쪽에서 오른쪽으로
            {
                transform.position = new Vector3(respawnPositionX, transform.position.y, transform.position.z);
            }
            else //false면 오른쪽에서 왼쪽으로
            {
                transform.position = new Vector3(-respawnPositionX, transform.position.y, transform.position.z);
            }
            tempTime2 = coolTime2;
        }
    }

    public bool GetRandomDirection()
    {
        int random = Random.Range(0, 2);
        if(random == 0)
            return true;
        else
            return false;
    }


    public void RespawnEnemy()
    {
        tempTime -= Time.deltaTime;
        if(tempTime < 0)
        {
            Instantiate(sideEnemys, transform.position, transform.rotation);
            tempTime = coolTime;
        }
    }
}
