using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hpbar : MonoBehaviour {
    private Transform targetCamera;
	// Use this for initialization
	void Start () {
        targetCamera = GameObject.Find("Main Camera").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(targetCamera);
	}
}
