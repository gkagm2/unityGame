using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {
    float gravity = -20.0f; // gravity
    float jumpSpeed = 10.0f; // jump speed
    Vector3 velocity; //current speed

    CharacterController controller;
	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        velocity = new Vector3(0, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        float y = transform.position.y;
        if(y < 1)
        {
            gameObject.transform.position = new Vector3(0.0f, 1.1f, 0.0f);
        }
        //캐릭터가 땅에 닿아 있는지 체크
        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                velocity.y = jumpSpeed;
            }
               
        }
        velocity.y += (gravity * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
	}
}
