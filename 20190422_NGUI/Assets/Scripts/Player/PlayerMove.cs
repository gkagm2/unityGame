using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    void Update()
    {
        if (Input.touchCount > 0)
        {
            transform.position = Input.GetTouch(0).position;
            Debug.Log(Input.GetTouch(0).position);
        }
    }
}