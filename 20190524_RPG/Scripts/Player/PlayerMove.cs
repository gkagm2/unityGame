using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    Animator anim;
    CharacterController character;

    [SerializeField] float moveSpeed;

    bool isMove;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        TryWalk();

    }

    private void TryWalk()
    {
        float _dirX = Input.GetAxisRaw("Horizontal"); //좌, 우
        float _dirZ = Input.GetAxisRaw("Vertical"); // 위, 아래

        Vector3 direction = new Vector3(_dirX, 0, _dirZ);

        isMove = false;
        if (direction != Vector3.zero)
        {
            isMove = true;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }

        anim.SetBool("Move", isMove);
        anim.SetFloat("DirX", direction.x);
        anim.SetFloat("DirZ", direction.z);
    }
}
