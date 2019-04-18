using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : SpriteBase { // inheritance SpriteBase class
    public GameObject Bullet;
    public float Speed = 5.0f;

    float LastShootTime;
    public float ShootDelayTime = 0.2f;
	// Use this for initialization
	void Start () {
        LastShootTime = Time.time;
        Debug.Log("LastShootime : " + LastShootTime);
	}
	
	// Update is called once per frame
	void Update () {
        float moveAmount = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        transform.Translate(Vector3.right * moveAmount);

        //0.2초마다 자동으로 총알을 발사하는 스크립트.
        if (Time.time > LastShootTime + ShootDelayTime)
        {
            LastShootTime = Time.time;
            // 복사를 원하는 오브젝트, 새 오브젝트의 위치, 새 오브젝트의 오리엔테이션
            Instantiate(Bullet, transform.position, transform.rotation);
        }
	}
}
