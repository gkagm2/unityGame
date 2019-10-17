using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mon_CharacterController : MonoBehaviour
{
    [Header("Character GameObject")]
    public Mon_CharacterAnimation characterAnim;             // Character animation
    public CharacterController characterController; // Character controller
    public Mon_CharacterCamera characterCamera;
    public GameObject characterModelObject;
    public GameObject playerObject;

    [Space]
    public Mon_CharacterState state;                // Character stat

    public float moveSpeed = 5.0f ;                 // Character move speed

    [Header("Movement State flag")]
    private bool isMove = false;                    // when character move flag change true
    private bool isDash = false;
    private bool isDefaultAttack = false;
    private bool isAimAttackStandby = false;
    private bool isAimAttackShoot = false;

    private EMon_CharacterState eCharacterState;
    


    // Start is called before the first frame update
    void Start()
    {
        // TODO (SagacityJang) : delete this default state
        state.SetDefaultState(); // set default character stat (also can be set in the Inspector screen)
        eCharacterState = EMon_CharacterState.idle;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    /// <summary>
    /// Character movement
    /// </summary>
    public void Movement()
    {
        // Init state flags
        InitStatsFlags();

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 movedDirection = new Vector3(h, 0, v);

        // 움직이는지 확인 후 상태 변환
        if (movedDirection.x != 0 || movedDirection.z != 0)
        {
            eCharacterState = EMon_CharacterState.move; // 움직이는 상태로 변환
            isMove = true;
        }
        else
        {
            eCharacterState = EMon_CharacterState.idle; // 멈춰있는 상태로 변환
            isMove = false;
        }
        characterAnim.SetIsMoveAnimation(isMove);


        characterModelObject.transform.eulerAngles = new Vector3(0, characterCamera.targetObject.transform.eulerAngles.y, 0);
        playerObject.transform.eulerAngles = new Vector3(0, characterCamera.targetObject.transform.eulerAngles.y, 0);

        movedDirection = characterCamera.targetObject.transform.TransformDirection(movedDirection); // 월드좌표계로 변환하여 movedDirection으로 넣음.
        characterController.SimpleMove(movedDirection.normalized * moveSpeed);
    }

    private void InitStatsFlags()
    {
        isDash = false;
        isDefaultAttack = false;
        //isAimAttackStandby = false;
        isAimAttackShoot = false;
    }

    /// <summary>
    /// Get the vector of direction when moving.
    /// </summary>
    /// <returns> Direction vector3</returns>
    public Vector3 GetDirectionVectorWhenMoveing()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //return new Vector3(h, 0, v);
        return new Vector3(h, 0, v);    
    }

    /// <summary>
    /// Rotate in the direction of movement.
    /// </summary>
    /// <param name="targetObject">GameObject to be rotated</param>
    /// <param name="movedDirection">Moved direction</param>
    public void RotateInTheDirectionOfMovement(GameObject targetObject, Vector3 movedDirection)
    {
        if (movedDirection.x == 0 && movedDirection.z == 0)
        {
            return;
        }
        targetObject.transform.eulerAngles = new Vector3(0, characterCamera.targetObject.transform.eulerAngles.y, 0);
    }

    public void AttackNormal()
    {
        isDefaultAttack = true;
        eCharacterState = EMon_CharacterState.defaultAttack;
        characterAnim.SetIsDefaultAttackAnimation();
    }

    public void AttackAimStandby()
    {
        Debug.Log("isAimAttackStandby : " + isAimAttackStandby);
        characterAnim.SetIsAimAttackStandbyAnimation(isAimAttackStandby);
    }

    public void AttackAimShoot()
    {
        isAimAttackStandby = false;
        //isAimAttackShoot = true;
        characterAnim.SetIsAimAttackShootAnimation();
    }

    public void Dash()
    {
        isDash = true;
        eCharacterState = EMon_CharacterState.dash;
        characterAnim.SetIsDashAnimation(true);
    }

}

public enum EMon_CharacterState
{
    idle,
    move,
    dash,
    defaultAttack,
    aimAttack
}