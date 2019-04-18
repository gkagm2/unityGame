using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunFireScript : MonoBehaviour {

    public float gunFireSpeed = 2;
    float time = 0.0f;
    public GameObject bulletObj;
    GameObject player;
    GameManager gameManager;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameMrg").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!gameManager.IsGameOver()) // 게임 종료가 아니면
        {
            time += Time.deltaTime;
            //총알 발사
            if (time > gunFireSpeed)
            {
                //플레이어보다 위에 있으면
                if (transform.position.y > player.transform.position.y)
                {
                    Instantiate(bulletObj, transform.position, transform.rotation); //총알을 쏜다.
                }
                time = 0;
            }

        }
	}
}
