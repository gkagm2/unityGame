using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    Animator anim;
    CharacterController controller;
    [SerializeField] float moveSpeed;
    [SerializeField] LayerMask layerMask;
    
    [SerializeField] float gravity;
    [SerializeField] float jumpPower;
    Vector3 gravityDirection = Vector3.zero;

    bool isMove;
    bool isJump;
    bool isFall;
    bool isBlock;
    bool isAttack;
    
    bool blockState = false;
    bool attackState = false;
    float dirX;
    float dirZ;
    
    float yVelocity = 0.0f;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        

        // 현재 캐릭터가 땅에 있는가?
        if (controller.isGrounded)
        {
            TryBlock(); // 막기
            TryAttack(); // 공격하기

            if (attackState || blockState) // 공격하거나 막는 상태이면
            {
                
            }
            else // 공격하거나 막는 상태가 아니면
            {
                // 위, 아래 움직임 셋팅. 
                TryWalk(); //걷기 가능
            }

            // 캐릭터 점프
            TryJump();


        }
        else //땅에 없으면
        {
            isJump = false;
            anim.SetBool("Jump", false);
        }
        yVelocity += (gravity * Time.deltaTime);
        gravityDirection.y = yVelocity;
        controller.Move(gravityDirection * Time.deltaTime);
    }
    
    // 걷기
    private void TryWalk()
    {
        dirX = Input.GetAxisRaw("Horizontal"); //좌, 우
        dirZ = Input.GetAxisRaw("Vertical"); // 위, 아래
        
        Debug.Log("dirX : " + dirX + " , dirZ : " + dirZ);
        Vector3 direction = new Vector3(dirX, 0, dirZ);

        isMove = false;
        if (direction != Vector3.zero)
        {
            isMove = true;
            
            transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
        }
        Debug.Log("direction : " + direction.normalized);
        anim.SetBool("Move", isMove);
        anim.SetFloat("DirX", direction.x);
        anim.SetFloat("DirZ", direction.z);
    }

    // 점프하기
    private void TryJump()
    {

        yVelocity = 0.0f;
        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            yVelocity = jumpPower;
            isJump = true;
            anim.SetBool("Jump",true);
            Debug.Log("Jump!");
        }
    }

    // 방패로 막기
    private void TryBlock()
    {
        if (isBlock == false)
        {
            anim.SetBool("Block", false);
            blockState = false;
        }

        isBlock = false;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("Shift key 누름");
            isBlock = true;
            blockState = true;
            anim.SetBool("Block", true);

        }
    }

    // 공격하기
    private void TryAttack()
    {
        if (isAttack == false)
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
