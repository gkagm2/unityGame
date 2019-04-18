using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRespawner : MonoBehaviour {
    public float[] coolTime;            //쿨타임 시간을 정렬해봄.
    public float respawnTime = 2.0f;     //리스폰 시간

    public float itemRespawnPosition = 13.0f; // 적이 리스폰 되는 위치


    [Header("Item object")]
    public GameObject enemyObject;



    GameManager gameManager;

    // Use this for initialization
    void Start()
    {
        coolTime = new float[1];
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }
    void ItemRespawn()
    {
        Instantiate(enemyObject, transform.position, transform.rotation);
    }
    void Update()
    {
        if (!gameManager.gameOver)
        {
            coolTime[0] += Time.deltaTime; //리스폰 쿨 타임

            if (coolTime[0] > Random.Range(respawnTime - 1.2f, respawnTime))
            {
                EnemyRespawnPosition(0);
                coolTime[0] = 0; //리스폰 쿨 타임 리셋.
            }
        }
    }


    //적 생성 위치
    void EnemyRespawnPosition(short respawnCase)
    {
        switch (respawnCase)
        {
            case 0:
                transform.position = new Vector3(itemRespawnPosition, Random.Range(0, 2), 0); // 랜덤으로 생성 됨.
                ItemRespawn();
                break;
            default:
                Debug.Log("EnemyRespawnPosition range : 0~2.   Error!!");
                break;
        }
    }
}
