using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour {

    public int hp = 5;

    public GameObject fireHoleObj;
    public GameObject bulletObj;

    [SerializeField] float moveSpeed = 4.0f;
    [SerializeField] float turnSpeed = 150.0f;
    


    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}


    // w,a,s,d -> only move
    public void Move(float dir_x = 0, float dir_z=0)
    {
        transform.Translate(dir_x * moveSpeed * Time.deltaTime, 0, dir_z * moveSpeed * Time.deltaTime);
    }

    // w,s -> move.   a,d -> turn 
    public void Move2(float dir_turn =0, float dir_z = 0)
    {
        transform.Translate( 0, 0, dir_z * moveSpeed * Time.deltaTime);
        transform.Rotate( 0, dir_turn * turnSpeed * Time.deltaTime, 0);
    }

    public void Fire(float destroyBulletCoolTime = 4.0f)
    {
        GameObject bullet = Instantiate(bulletObj, fireHoleObj.transform.position, fireHoleObj.transform.rotation) as GameObject;
        if (bullet)
        {
            Destroy(bullet, destroyBulletCoolTime);
        }
    }




}
