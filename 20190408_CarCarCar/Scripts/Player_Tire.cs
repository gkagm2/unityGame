using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Tire : MonoBehaviour {

    public float tire;
    
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        
        transform.Rotate(new Vector3(tire, 0.0f, 0.0f));
	}
}
