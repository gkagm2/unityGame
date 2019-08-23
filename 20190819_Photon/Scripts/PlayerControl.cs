using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;
using UnityEngine.UI;
public class PlayerControl : MonoBehaviour {

    public float speed = 5.0f;
    public float rotSpeed = 120.0f;

    private Transform tr;

    private PhotonView pv;

    public Material[] _material;
    private Vector3 currPos;
    private Quaternion currRot;

    public Transform firePos;
    public GameObject bullet;
    public GameObject iceBullet;
    public GameObject deadItem;

    public Image hpBar;

    private float maxHp = 50.0f;
    public float hp = 50.0f;



    // Use this for initialization
    void Start () {
        tr = GetComponent<Transform>();
        pv = GetComponent<PhotonView>();

        if (pv.isMine){
            // 자신의 플레이어에게만 카메라 제어권을 연결한다.
            GameObject.Find("Main Camera").GetComponent<SmoothFollow>().target = tr;
            this.GetComponent<Renderer>().material = _material[0];
        } else
        {
            this.GetComponent<Renderer>().material = _material[1];
        }

        // 동기화 콜백함수가 발생하려면 반드시 본 스크립트를 연결 시켜준다.
        pv.ObservedComponents[0] = this;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (pv.isMine)
        {

            /* Fire */
            if (Input.GetButtonDown("Fire1"))
            {
                Fire();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                Fire2();
            }
            
            /* Move */
            // 자신의 플레이어만 키보드 조작을 허용한다.
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            tr.Translate(Vector3.forward * v * Time.deltaTime * speed);
            tr.Rotate(Vector3.up * h * Time.deltaTime * rotSpeed);
        }
        else
        {
            // 네트워크로 연결된 다른 유저일 경우에는 실시간 전송 받는 변수를 이용해 이동시켜준다.
            tr.position = Vector3.Lerp(tr.position, currPos, Time.deltaTime * 10.0f);
            tr.rotation = Quaternion.Lerp(tr.rotation, currRot, Time.deltaTime * 10.0f);
            Debug.Log("tr.position : " + tr.position + ", tr.rotation : " + tr.rotation);
        }
    }

    private void Fire()
    {
        StartCoroutine(this.CreateBullet());
        pv.RPC("FireRPC", PhotonTargets.Others); // 자신의 PC만 제어하고 다른 PC만 FireRPC 함수를 실행 시키게 한다.
    }


    [PunRPC] 
    void FireRPC()
    {
        StartCoroutine(CreateBullet());
    }
    IEnumerator CreateBullet()
    {
        Instantiate(bullet, firePos.position, firePos.rotation);
        yield return null;
    }

    [PunRPC]
    private void Fire2()
    {
        StartCoroutine(CreateIceBullet());
    }
    IEnumerator CreateIceBullet()
    {
        Instantiate(iceBullet, firePos.position, firePos.rotation);
        yield return null;
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) // 동기화 콜백함수
    {
        if (stream.isWriting)
        {
            // 자신의 플레이 정보는 다른 네트워크 사용자에게 송신한다.
            stream.SendNext(tr.position);
            stream.SendNext(tr.rotation);
        } else
        {
            currPos = (Vector3)stream.ReceiveNext();
            currRot = (Quaternion)stream.ReceiveNext();
        }
    }

    [PunRPC]
    void TakeDamage(float damage)
    {
        StartCoroutine(CoTakeDamage(damage));
    }

    IEnumerator CoTakeDamage(float damage)
    {
        hp -= damage;
        
        if (hp <= 0)
        {
            GameObject moonStonitem = Instantiate(deadItem, transform.position, transform.rotation) as GameObject;
            if (moonStonitem)
            {
                Destroy(moonStonitem, 3f);
            }
            Destroy(gameObject);
        }
        hpBar.fillAmount = hp / maxHp;
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Bullet"))
        {
            TakeDamage(other.GetComponent<BulletControl>().damage);
        }
    }
}
