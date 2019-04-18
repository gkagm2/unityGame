using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawner : MonoBehaviour {
    public bool tempDelayTimeFlag= true;
    public float beforeDelayTime;
    public float coolTime;
    public float delayTime;
    public GameObject enemy;
    PlayerControl playerControl;

    GameManager gameManager;

	// Use this for initialization
	void Start () {
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        beforeDelayTime = delayTime;
	}
	
	// Update is called once per frame
	void Update () {
        //게임 오버가 아니면 
        if (!gameManager.gameOver)
        {
            coolTime += Time.deltaTime;

            if (coolTime > delayTime)
            {
                Instantiate(enemy, transform.position, transform.rotation);

                EnemyRespawnPosition(Random.Range(0, 4));
                //RandomPos();
                coolTime = 0;
            }
            
        }
		
	}
    //ItemRespawner랑 겹침 하나로 합치는 방법이 없을까
    //랜덤으로 리스폰할 위치값을 받아온다.
    void RandomPos()
    {
        transform.position = new Vector3(   Random.Range(-playerControl.moveLimit, playerControl.moveLimit), 0.30004f, 40);
    }
    void EnemyRespawnPosition(int random)
    {
        switch (random)
        {
            case 0:
                transform.position = new Vector3(3.41f, 0.30004f, 40);
                break;
            case 1:
                transform.position = new Vector3(1.35f, 0.30004f, 40);
                break;
            case 2:
                transform.position = new Vector3(-1.24f, 0.30004f, 40);
                break;
            case 3:
                transform.position = new Vector3(-3.39f, 0.30004f, 40);
                break;
            default:
                Debug.Log("EnemyRespawnPosition Error");
                break;
        }
    }
}
