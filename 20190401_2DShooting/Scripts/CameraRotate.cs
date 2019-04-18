using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour {
    public float Speed = 0.5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // what this code doesn't work?
        //transform.RotateAround(Vector3.up, Time.deltaTime * Speed);
        transform.RotateAround(Vector3.up, Vector3.up, Time.deltaTime * Speed);
	}
}
