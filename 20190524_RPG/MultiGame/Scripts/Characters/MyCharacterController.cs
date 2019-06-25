using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacterController : MonoBehaviour {

    public Animator anim;
    public float directionDampTime;
    public float speed = 6.0f;
    public float h = 0f;
    public float v = 0f;
    public bool jump = false;
    public bool die = false;


	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>() as Animator;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            
        }
	}
}
