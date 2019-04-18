using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootControl : MonoBehaviour {

    //총알 오브젝트
    public GameObject bulletObject;

    //cool time
    // 0 : Shooting cooltime
    [Header("0 : 총알 생성 coolTime")]
    public float[] coolTime;
    float[] tempTime;

    // Use this for initialization
    void Start () {
        tempTime = new float[coolTime.Length];
        for (int i = 0; i < coolTime.Length; i++)
        {
            tempTime[i] = coolTime[i];
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!GameManager.isGameOver)
        {
            Shoot();
        }
        else
        {
        }
    }

    public void Shoot()
    {
        tempTime[0] -= Time.deltaTime;
        if (tempTime[0] < 0)
        {
            //총알 생성.
            Instantiate(bulletObject, transform.position, transform.rotation);
            tempTime[0] = coolTime[0];
        }
    }
}
