using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour {
    public float effectDelayTime;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, effectDelayTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
