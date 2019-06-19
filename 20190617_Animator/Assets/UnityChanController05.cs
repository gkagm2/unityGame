using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController05 : MonoBehaviour {

    Animator anim;
    int dirForwardHash = Animator.StringToHash("Dir_Forward");
    int moveHash = Animator.StringToHash("Move");
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {


        
        float dir_f = Input.GetAxis("Vertical");
        Debug.Log("dir_f :  " + dir_f);
        if (dir_f != 0) // move
        {
            anim.SetBool(moveHash, true);
            anim.SetFloat(dirForwardHash, dir_f);

            
            
        }
        else { // just stand
            anim.SetBool(moveHash, false);
        }
	}
}
