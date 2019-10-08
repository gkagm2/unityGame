using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour {
    public Transform cameraTransform;
    public PlayerModelAnimController modelAnimController;

    public float moveSpeed = 10.0f;
    public float jumpSpeed = 10.0f;
    public float gravity = -20.0f;

    CharacterController characterController = null;
    float yVelocity = 0.0f;

    //플레이어 죽음 상태 처리
    PlayerState playerHealth = null;


	// Use this for initialization
	void Start () {
        characterController = GetComponent<CharacterController>();
        playerHealth = GetComponent<PlayerState>();
	}

    // Update is called once per frame

    void Update()
    {
        if (playerHealth.isDead) //플레이어가 죽으면
            return;


        //포탄 처리
        float y = transform.position.y;
        if (y < 0)
            gameObject.transform.position = new Vector3(0f, 40f, 0f);

        //움직임 처리
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        modelAnimController.StartMoveAnimation(x, z); // 움직임 애니메이션 

        Vector3 moveDirection = new Vector3(x, 0, z);
        moveDirection = cameraTransform.TransformDirection(moveDirection); //월드좌표계로 변환하여 moveDirection으로 넣음.

        moveDirection *= moveSpeed;
        if (characterController.isGrounded)
        {
            yVelocity = 0.0f;
            if (Input.GetButtonDown("Jump")) //점프면
            {
                yVelocity = jumpSpeed; //점프 스피드를 y속도값에 넣음
            }
        }
        yVelocity += (gravity * Time.deltaTime); //중력에 의하여 점점 스피드가 줄어듬.
        moveDirection.y = yVelocity; //y값에 y속도값을 넣는다.

        characterController.Move(moveDirection * Time.deltaTime); //컨트롤러.Move를 통하여 움직인다.
    }
}
