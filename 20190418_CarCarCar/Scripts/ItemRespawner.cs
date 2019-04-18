using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRespawner : MonoBehaviour {
    public float coolTime;
    public float delayTime;
    // Use this for initialization
    PlayerControl playerControl;
    public GameObject item;

    GameManager gameManager;

	void Start () {
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {

        //게임오버가 아니면
        if (!gameManager.gameOver)
        {
            coolTime += Time.deltaTime;
            if (coolTime > delayTime)
            {
                Instantiate(item, transform.position, transform.rotation);
                RandomPos();
                coolTime = 0;
            }
        }
        
	}
    //ItemRespawner랑 겹침 하나로 합치는 방법이 없을까
    void RandomPos()
    {
        transform.position = new Vector3(Random.Range(-playerControl.moveLimit, playerControl.moveLimit), 0, 40);
    }
}
