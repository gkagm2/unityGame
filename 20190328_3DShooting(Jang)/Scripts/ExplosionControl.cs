using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExplosionControl : MonoBehaviour {
    public bool isDestroy = false;
    public float DestroyTime = 2.0f;//몇 초 후에 자동으로 삭제해주기.
	// Use this for initialization
	void Start () {
        if(isDestroy == true)
        {
            Destroy(gameObject, DestroyTime);
        }

    }
}
