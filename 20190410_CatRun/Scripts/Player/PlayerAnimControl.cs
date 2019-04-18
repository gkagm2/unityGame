using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimControl : MonoBehaviour {

    public static short Exflag= 4;
    
    public Animation catAnim;

    /// <summary>
    /// Exflag가
    /// 0 : 애니메이션 조작 및 정보
    /// 1 : 애니메이션 블렌딩
    /// 2 : 애니메이션 레이어
    /// 3 : 애니메이션 믹싱
    /// 4 : 애니메이션 추가
    /// </summary>
    //애니메이션 레이어



    // Use this for initialization
    void Start () {
        switch (Exflag)
        {
            case 0:

                break;
            case 1:
                
                break;
            case 2:
                
                catAnim.wrapMode = WrapMode.Loop;
                catAnim["Jump"].wrapMode = WrapMode.Once;
                catAnim["Jump"].layer = 1;

                catAnim.Stop();
                break;
            case 3: //TODO : error뜨는데 어떻게 해야되는지 모르겠다.
                Transform mixTransform;
                mixTransform = transform.Find("kittenRoot/Hips/LeftUpLeg");
                mixTransform = transform.Find("kkittenRoot/Hips/RightUpLeg"); 
                 catAnim["Walk"].AddMixingTransform(mixTransform);
                break;
            case 4:
                
                break;
            default:
                break;

        }
        
	}
	
	// Update is called once per frame
	void Update () {
        switch (Exflag)
        {
            case 0:
                Ex0();
                break;
            case 1:
                Ex1();
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                Ex4();
                break;
            default:
                break;
        }
        
        
    }

    void Ex0()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            catAnim.Play("Idle");

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            catAnim.Play("Walk");
            catAnim["Walk"].speed = 0.2f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            catAnim.Play("Run");
            catAnim["Run"].speed = 2.0f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            catAnim.Play("Jump");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            catAnim.Play("Ithcing");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            catAnim.Play("Meow");
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            catAnim.Play("IdleSit");


            catAnim["Walk"].wrapMode = WrapMode.Once; //이런식으로 고칠 수 있음.
        }
        Debug.Log("catAnim[Walk].clip.length : " + catAnim["Walk"].clip.length);
        Debug.Log("catAnim[Walk].clip.frameRate : " + catAnim["Walk"].clip.frameRate);
        Debug.Log("catAnim[Walk].clip.wrapMode : " + catAnim["Walk"].clip.wrapMode);
        Debug.Log("catAnim[Walk].clip.length : " + catAnim["Walk"].clip.length);

        Debug.Log("catAnim[Walk].wrapMode : " + catAnim["Walk"].wrapMode);
        Debug.Log("catAnim[Walk].time : " + catAnim["Walk"].time);
        Debug.Log("catAnim[Walk].normalizedTime : " + catAnim["Walk"].normalizedTime);
        Debug.Log("catAnim[Walk].speed : " + catAnim["Walk"].speed);
        Debug.Log("catAnim[Walk].normalizedSpeed : " + catAnim["Walk"].normalizedSpeed);
        Debug.Log("catAnim[Walk].length : " + catAnim["Walk"].length);
        Debug.Log("catAnim[Walk].layer : " + catAnim["Walk"].layer);
        Debug.Log("catAnim[Walk].clip : " + catAnim["Walk"].clip);
        Debug.Log("catAnim[Walk].name : " + catAnim["Walk"].name);
        Debug.Log("catAnim.enabled : " + catAnim.enabled);
        Debug.Log("catAnim.name : " + catAnim.name);
        Debug.Log("catAnim.clip : " + catAnim.clip);
    }
    void Ex1()
    {
        //애니메이션 블렌딩
        if(Input.GetAxis("Vertical") < 0.0f)
        {
            catAnim.CrossFade("Jump");
        }
        else
        {
            catAnim.CrossFade("Walk");
        }
    }
    void Ex4()
    {

    }
}
