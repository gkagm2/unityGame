using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Character animation class
/// </summary>
public class Mon_CharacterAnimation : MonoBehaviour
{
    public Animator animator;   // Character animator
    private int isMoveHash = Animator.StringToHash("isMove"); // animator hash key
    private int isDashHash = Animator.StringToHash("isDash");
    private int isDefaultAttackHash = Animator.StringToHash("isDefaultAttack");
    private int isAimAttackStandbyHash = Animator.StringToHash("isAimAttackStandby");
    private int isAimAttackShootHash = Animator.StringToHash("isAimAttackShoot");

    /// <summary>
    /// Set move animation with on off flag parameter
    /// </summary>
    /// <param name="flag">on off flag</param>
    public void SetIsMoveAnimation(bool flag)
    {
        animator.SetBool(isMoveHash, flag);
    }
    /// <summary>
    /// Set dash animation with on off flag parameter
    /// </summary>
    /// <param name="flag">on off flag</param>
    public void SetIsDashAnimation(bool flag)
    {
        animator.SetBool(isDashHash, flag);
    }
    /// <summary>
    /// Set default attack animation with on off flag parameter
    /// </summary>
    /// <param name="flag">on off flag</param>
    public void SetIsDefaultAttackAnimation()
    {
        animator.SetTrigger(isDefaultAttackHash);
    }

    /// <summary>
    /// Set aim attack standby animation with on off flag parameter
    /// </summary>
    /// <param name="flag"></param>
    public void SetIsAimAttackStandbyAnimation(bool flag)
    {
        animator.SetBool(isAimAttackStandbyHash, flag);
    }

    /// <summary>
    /// Set aim attack shoot animation with on off flag parameter
    /// </summary>
    /// <param name="flag"></param>
    public void SetIsAimAttackShootAnimation()
    {
        animator.SetTrigger(isAimAttackShootHash);
    }
}
