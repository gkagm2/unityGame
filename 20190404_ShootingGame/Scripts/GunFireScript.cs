using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFireScript : MonoBehaviour {
    enum fireType { NOMAL, HEAVY, FAST };
    [Header("Auto fire control")]
    public float gunFireSpeed= 0.5f;
    float time = 0.0f;

    [Header("Gun control")]
    public GameObject bulletObj;
    public bool autoFire = false;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update() {

        //A를 누르면 Auto
        if (Input.GetKeyDown(KeyCode.A))
        {
            print("A를 눌렀음");
            //autoFire 바꾸기
            if (autoFire) 
                autoFire = false;
            else
                autoFire = true;
        }

        time += Time.deltaTime;

        //자동 발사 True면
        if (autoFire == true)
        {
            if (time > gunFireSpeed)
            {
                FireBullet(fireType.FAST); //평범한 쏘기
                time = 0;
            }

        }//수동 발사면
        else
        {
            if (Input.GetKeyDown(KeyCode.Space)) // 스페이스바를 누르면
            {
                if (time > gunFireSpeed)
                {
                    FireBullet(fireType.HEAVY); //평범한 쏘기
                    time = 0;
                }
            }
        }
        
        
	}
    void FireBullet(fireType type) {
        switch (type)
        {
            case fireType.NOMAL:
                GetComponent<AudioSource>().Play();// 효과음 시작
                Instantiate(bulletObj, transform.position, transform.rotation); // 총알 생성
                break;

            case fireType.HEAVY:
                GetComponent<AudioSource>().Play();// 효과음 시작
                Instantiate(bulletObj, transform.position, transform.rotation);
                Instantiate(bulletObj, transform.position + new Vector3(-0.3f,0.0f,0.0f), transform.rotation);
                Instantiate(bulletObj, transform.position + new Vector3(0.3f, 0.0f, 0.0f), transform.rotation);
                break;

            case fireType.FAST:
                GetComponent<AudioSource>().Play();// 효과음 시작
                Instantiate(bulletObj, transform.position, transform.rotation); // 총알 생성
                break;

            default:
                Debug.Log("GunFireScript FireBullet error");
                break;
        }
        
    }
}
