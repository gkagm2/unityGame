using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, 0, -40 * Time.deltaTime);
        Debug.Log(Time.deltaTime);
    }
}
