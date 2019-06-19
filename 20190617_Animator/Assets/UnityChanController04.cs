using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController04 : MonoBehaviour {

    Animator anim;

    int slideHash = Animator.StringToHash("slide");
    int jumpHash = Animator.StringToHash("jump");

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update() {
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetTrigger(slideHash);

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("SLIDE00"))
            {
                anim.SetTrigger(jumpHash);
                
            }
        }


	}
}
