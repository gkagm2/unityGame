using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
    public EnemyControl enemy;
    [Header("bullet control")]
    public float speed = 2.0f;
    GameObject player;
    
    Vector3 myV = new Vector3();
    Vector3 playerPosition;
    void Start () {
        player = GameObject.Find("Player");
        playerPosition = player.transform.position;
        myV = (playerPosition - transform.position) * Time.deltaTime; //player에서 enemy를 뺌.
    }

	// Update is called once per frame
	void Update () {
        transform.Translate(myV);
        Destroy(gameObject, 3); //3초 후에 사라짐
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player"){ //Player어와 충돌 시
            Destroy(gameObject);
        }
    }


}
