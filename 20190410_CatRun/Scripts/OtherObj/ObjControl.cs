using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjControl : MonoBehaviour {
    MovementManager movementManager; //게임 매니저
    //GameManager gameManager;

    public float enemyDistanceLimit = -13.0f;
    

    


    // Use this for initialization
    void Start () {
        movementManager = GameObject.Find("GameManager").GetComponent<MovementManager>(); // 속도 매니저를 불러옴
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); // 게임 매니저를 불러옴
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(-movementManager.enemySpeed * Time.deltaTime, 0, 0);

        if (transform.position.x < enemyDistanceLimit) //제한된 거리까지 가면 
            Destroy(gameObject); //파괴 됨
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
