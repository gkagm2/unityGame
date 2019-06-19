using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController06 : MonoBehaviour {

    

    // parameters
    public int speed;
    public float sideSpeed = 5.0f;
    public float jumpSpeed = 7.0f;

    public float jumpVelocity;
    public float characterControllerHeight;

    
    [SerializeField] float gravity = 3.7f;       // character gravity

    // animator hash
    int dirForwardHash = Animator.StringToHash("Dir_Forward");
    int dirRightHash = Animator.StringToHash("Dir_Right");
    int moveHash = Animator.StringToHash("Move");
    int jumpHash = Animator.StringToHash("Jump");
    int slideHash = Animator.StringToHash("Slide");

    float timeOfAnimation;

    // references
    Animator anim;
    CharacterController characterController;



    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        timeOfAnimation = GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;

        characterControllerHeight = characterController.height;
    }

 

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKey(KeyCode.Q))
        {
            anim.SetTrigger("Attack");
            
            if (anim.GetCurrentAnimatorStateInfo(1).IsName("Punch") || anim.GetCurrentAnimatorStateInfo(1).normalizedTime == 0.5f)
            {
                anim.Play("Punch", 1, 0.5f);
                
                //anim.SetFloat("normalizedTime", 0.5f);
            }
        }
        else
        {
            anim.ResetTrigger("Attack");
            
            if (anim.GetCurrentAnimatorStateInfo(1).IsName("Punch"))
            {
                //anim.SetFloat("spsped", 1f);
            }
        }


    

        // input slide key
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetTrigger(slideHash);
        }



        float dir_f = Input.GetAxis("Vertical"); // f : forward
        float dir_r = Input.GetAxis("Horizontal"); // r : right
        
        if (dir_f != 0)
        {
            // movement animation
            anim.SetBool(moveHash, true);
            anim.SetFloat(dirForwardHash, dir_f);
            anim.SetFloat(dirRightHash, dir_r);


        }
        else
        { // just stand
            anim.SetBool(moveHash, false);
        }

        if(characterController.isGrounded)
        {
            // jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpVelocity = Time.deltaTime * jumpSpeed;
                anim.SetTrigger(jumpHash);
            }
            
        }
        jumpVelocity -= (Time.deltaTime *gravity);
        // Movement
        characterController.Move(new Vector3(dir_r * sideSpeed * Time.deltaTime, jumpVelocity, dir_f * speed * Time.deltaTime));
    }
    // Animation event
    public void AEvent_SlideStart()
    {
        characterController.height = characterControllerHeight / 2;
        characterController.center = new Vector3(0, 0.4f, 0);
        Debug.Log("sdsdsdsdsdsdsdsdsd");
        StartCoroutine(C_ResetCharacterController());
    }
    IEnumerator C_ResetCharacterController()
    {
        yield return new WaitForSeconds(0.4f);
        characterController.height = characterControllerHeight;
        characterController.center = new Vector3(0, 0.83f, 0);
    }

}
