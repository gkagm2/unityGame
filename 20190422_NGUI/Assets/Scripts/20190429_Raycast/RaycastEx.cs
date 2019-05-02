using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastEx : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if(Physics.Raycast(transform.position, fwd, 10))
        {
            Debug.Log("There is something in fron of the object!");
        }
        Debug.DrawRay(transform.position, fwd * 7f, Color.red);
	}

}
