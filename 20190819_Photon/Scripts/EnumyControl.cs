using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections;
using UnityEngine.UI;

public class EnumyControl : MonoBehaviour {

    private PhotonView pv;
    private NavMeshAgent nvAgent;
    private Transform tr;
    private Transform target;

    public GameObject[] players;
    public Vector3 curPos;
    public Quaternion curRot;
    public float traceTime = 0.5f;

    private float maxHp = 50.0f;
    public float hp = 50.0f;

    // effect
    public GameObject spawnEffect;

    public Image hpBar;


	// Use this for initialization
	void Start () {
        Init();

        pv = PhotonView.Get(this);
        pv.ObservedComponents.Add(this);

        nvAgent = GetComponent<NavMeshAgent>();
        tr = GetComponent<Transform>();
        if (PhotonNetwork.isMasterClient)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            target = players[0].transform;

            curPos = tr.position;

            float dist = (target.position - tr.position).sqrMagnitude;
            foreach( GameObject _player in players)
            {
                if((_player.transform.position - tr.position).sqrMagnitude < dist)
                {
                    target = _player.transform;
                    break;
                }
            }
            nvAgent.destination = target.position;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (PhotonNetwork.isMasterClient)
        {
            if(Time.time > traceTime)
            {
                players = GameObject.FindGameObjectsWithTag("Player");

                float dist = (target.position - tr.position).sqrMagnitude;
                foreach(GameObject _player in players)
                {
                    if((_player.transform.position - tr.position).sqrMagnitude < dist)
                    {
                        target = _player.transform;
                        break;
                    }
                }
                nvAgent.destination = target.position;
                traceTime = Time.time + 0.5f;
            }
        } else
        {
            tr.position = Vector3.Lerp(tr.position, curPos, Time.deltaTime * 10f);
            tr.rotation = Quaternion.Lerp(tr.rotation, curRot, Time.deltaTime * 10f);
        }
	}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) // 동기화 콜백함수
    {
        if (stream.isWriting) // 내 플레이어 정보를 다른 네트워크 사용자에게 송신한다.
        {
            stream.SendNext(tr.position);
            stream.SendNext(tr.rotation);
        }
        else // 타플레이어의 정보를 수신한다.
        {
            curPos = (Vector3)stream.ReceiveNext();
            curRot = (Quaternion)stream.ReceiveNext();
        }
    }

    // 초기화
    //[PunRPC]
    public void Init()
    {
        StartCoroutine(CoInit());
    }
    IEnumerator CoInit()
    {
        Instantiate(spawnEffect, transform.position, transform.rotation); // 마법진 생성
        yield return null;
    }
    
    // 데미지를 입는다.
    [PunRPC]
    public void TakeDamage(float damage)
    {
        StartCoroutine(CoTakeDamage(damage));
    }
    IEnumerator CoTakeDamage(float damage)
    {

        hp -= damage;
        
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
        hpBar.fillAmount = hp / maxHp;
        yield return null;
    }

    // 충돌 시
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Bullet"))
        {
            TakeDamage(other.GetComponent<BulletControl>().damage);
        }
    }
}
