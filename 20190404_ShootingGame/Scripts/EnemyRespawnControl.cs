using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnControl : MonoBehaviour {
    GameManager gameManager;

    [Header("Enemy respawn time")]
    public float enemyRespawnCoolTime = 0.0f;
    public float enemyRespawnTime = 0.5f;

    [Header("EnemyRespawn contorl")]
    public float movementDistance = 3.0f;
    public GameObject enemyObj;
    public float respawnHeight = 12.0f;

    [Header("Level control")]
    int level;

    void Start()
    {
        gameManager = GameObject.Find("GameMrg").GetComponent<GameManager>();

        
    }
    // Update is called once per frame
    void Update()
    {
        if (!gameManager.IsGameOver())
        {
            //레벨이 올라갈때마다 1씩증가
            
            enemyRespawnCoolTime += Time.deltaTime; //적 리스폰 쿨타임
            //리스폰
            if (enemyRespawnCoolTime > enemyRespawnTime - (GameObject.Find("GameMrg").GetComponent<GameManager>().level-1) * 0.1f)
            {
                Instantiate(enemyObj, transform.position, transform.rotation);
                enemyRespawnCoolTime = 0;
            }
            transform.position = new Vector3(Random.Range(-movementDistance, movementDistance), respawnHeight, 0.0f);
            

        }
        
    }
}
