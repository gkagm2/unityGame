using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public GameObject enemy;

    //enemy spawn time
    public float spawnTime = 2.0f;
    float deltaSpawnTime = 0.0f;

    //enemy spawn count
    int spawnCnt = 1;
    int maxSpawnCnt = 10;

    //적 매니저 최적화 풀링 시스템 구현
    GameObject[] enemyPool;
    int poolSize = 10;


	// Use this for initialization
	void Start () {
        enemyPool = new GameObject[poolSize];
        for(int i = 0; i < poolSize; ++i)
        {
            enemyPool[i] = Instantiate(enemy) as GameObject;
            enemyPool[i].SetActive(false);
        }
	}

    // Update is called once per frame
    void Update()
    {

        deltaSpawnTime += Time.deltaTime;

        if (deltaSpawnTime > spawnTime)
        {
            deltaSpawnTime = 0f;
            for (int i = 0; i < poolSize; i++)
            {
                if (enemyPool[i].activeSelf == true)
                {
                    continue;
                }
                int x = Random.Range(-20, 20);
                enemyPool[i].transform.position = new Vector3(x, 0.1f, 20f);
                enemyPool[i].SetActive(true);

                enemyPool[i].name = "Enemy_" + spawnCnt;

                ++spawnCnt;
                break;
            }



            ////최대치까지 생성했으면 
            //if (spawnCnt > maxSpawnCnt) 
            //    return;

            //deltaSpawnTime += Time.deltaTime;
            //if(deltaSpawnTime > spawnTime)
            //{
            //    deltaSpawnTime = 0.0f;

            //    GameObject enemyObj = Instantiate(enemy) as GameObject;
            //    float x = Random.Range(-20.0f, 20.0f);
            //    enemyObj.transform.position = new Vector3(x, 0.1f, 20.0f);  

            //    enemyObj.name = "Enemy_" + spawnCnt;
            //    ++spawnCnt;
        }
    }
}
