using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSprite : SpriteBase {
    public float Speed = 10.0f;
	// Use this for initialization
	void Start () {
        gameObject.AddComponent<Rigidbody>();

        //Gravity 사용 안함.
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        //gameObject.rigidbody.useGravity = false; //구버전

        //BoxCollider를 추가.
        gameObject.AddComponent<BoxCollider>();

        //Collider를 Trigger로 설정
        gameObject.GetComponent<Collider>().isTrigger = true;
	}

	
	// Update is called once per frame
	void Update () {
        float moveAmount = Speed * Time.deltaTime;
        transform.Translate(Vector3.up * moveAmount);
        if (transform.position.y > 10.0f)
        {
            Destroy(gameObject);
        }
		
	}
}
