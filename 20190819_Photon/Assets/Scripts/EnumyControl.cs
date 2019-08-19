using UnityEngine;
using UnityEngine.AI;

public class EnumyControl : MonoBehaviour {

    private PhotonView pv;
    private NavMeshAgent nvAgent;
    private Transform tr;
    private Transform target;

    public GameObject[] players;
    public Vector3 curPos;
    public Quaternion curRot;
    public float traceTime = 0.5f;


	// Use this for initialization
	void Start () {
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
}
