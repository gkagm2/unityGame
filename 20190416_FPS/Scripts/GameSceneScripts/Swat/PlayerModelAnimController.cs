using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelAnimController : MonoBehaviour {

    public Animator anim;
    public CharacterMove characterMove;

    private int isFireHash = Animator.StringToHash("isFire");
    private int horizontalHash = Animator.StringToHash("Horizontal");
    private int verticalHash = Animator.StringToHash("Vertical");

    /// <summary>
    /// 공격 애니메이션을 시작한다.
    /// </summary>
    public void StartFireAnimation()
    {
        anim.SetTrigger(isFireHash);
    }

    /// <summary>
    /// 움직임 애니메이션을 시작한다.
    /// </summary>
    /// <param name="horizontal"></param>
    /// <param name="vertical"></param>
    public void StartMoveAnimation(float horizontal, float vertical)
    {
        anim.SetFloat(horizontalHash, horizontal);
        anim.SetFloat(verticalHash, vertical);
    }
}
