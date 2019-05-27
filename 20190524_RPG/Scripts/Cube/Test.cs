using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    Animator anim;
    Rigidbody rigid;
    BoxCollider col;

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] LayerMask layerMask;

    bool isMove;
    bool isJump;
    bool isFall;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {

        TryWalk();
        TryJump();

	}

    private void TryWalk()
    {
        float _dirX = Input.GetAxisRaw("Horizontal"); // 좌측 -1 우측 1 안누르면 0 리턴
        float _dirZ = Input.GetAxisRaw("Vertical"); // 위 1 아래 -1 안누르면 0 리턴


        Vector3 direction = new Vector3(_dirX, 0, _dirZ);
        isMove = false;
        if (direction != Vector3.zero)
        {
            isMove = true;
            // 대각선 움직임도 수평 수직 움직임과 똑같이 만들기 위해서 normalized 함. 하지 않으면 대각선 속도만 유독 빨라짐.
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }

        anim.SetBool("Move", isMove);
        anim.SetFloat("DirX", direction.x);
        anim.SetFloat("DirZ", direction.z);
    }

    private void TryJump()
    {

        if (isJump)
        {
            if(rigid.velocity.y <= 0.1f && !isFall)
            {
                isFall = true;
                anim.SetTrigger("Fall");
            }
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position,-transform.up, out hitInfo, col.bounds.extents.y + 0.05f, layerMask)){
                anim.SetTrigger("Land");
                isJump = false;
                isFall = false;
            }
        }   

        if(Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            isJump = true;
            //rigid.velocity = new Vector3(0, jumpForce, 0);
            rigid.AddForce(Vector3.up * jumpForce);
            anim.SetTrigger("Jump");
        }
    }
}
