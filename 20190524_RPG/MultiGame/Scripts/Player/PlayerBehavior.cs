using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {
    Animator anim;
    
    bool isBlock;
    bool isAttack;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        TryBlock();
        TryAttack();
    }

    // 방패로 막기
    private void TryBlock()
    {
        if (isBlock == false)
        {
            anim.SetBool("Block", false);
        }

        isBlock = false;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("Shift key 누름");
            isBlock = true;
            anim.SetBool("Block", true);

        }
    }

    // 공격하기
    private void TryAttack()
    {
        if(isAttack== false)
        {
            anim.SetBool("Attack", false);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Attack key 누름");
            isAttack = true;
            anim.SetBool("Attack", true);
        }
        isAttack = false;
    }
}
