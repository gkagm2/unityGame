using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {

    Animator anim;



	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        // 플레이어의 Weapon에 닿으면
        if (collision.gameObject.tag == "PlayerWeapon")
        {
            HitReaction();

        }
    }

    private void HitReaction()
    {
        anim.SetBool("Impact",true);
    }




}

