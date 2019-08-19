using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PhotonInit : MonoBehaviour {

    private const string photonProjectName = "TestPhotonGame";

    public Text logText;

    private void Awake()
    {
        // 포톤네트워크에 버전별로 분리하여 접속한다.
        PhotonNetwork.ConnectUsingSettings(photonProjectName);
    }
    
    public virtual void OnConnectedToMaster()
    {
        Debug.Log("마스터에 접속함!");
        PhotonNetwork.JoinRandomRoom();
    }

    // 로비에 입장하였을 때 호출되는 콜백함수
    public virtual void OnJoinedLobby() // 로비에 입장시 호출되는 콜백함수
    {
        Debug.Log("로비에 입장함!");
        PhotonNetwork.JoinRandomRoom();
    }

    // 랜덤 룸 입장에 실패하였을 때 호되는 콜백함수
    public virtual void OnPhotonRandomJoinFailed()
    {
        Debug.Log("방이 없음");
        PhotonNetwork.CreateRoom("MyRoom");
    }

    // 룸을 생성완료 하였을 때 호출되는 콜백함수
    public virtual void OnCreatedRoom()
    {
        Debug.Log("방 만들어짐!");
    }

    // 룸에 입장되었을 경우 호출되는 콜백함수
    public virtual void OnJoinedRoom()
    {
        Debug.Log("방에 입장함!");
        StartCoroutine(CreatePlayer());
    }

    private void Update()
    {
        logText.text = PhotonNetwork.connectionStateDetailed.ToString();
    }

    IEnumerator CreatePlayer()
    {
        // prefabs를 name으로 찾는다.
        PhotonNetwork.Instantiate("MyPlayer", new Vector3(0, 1, 0), Quaternion.identity, 0);
        yield return null;
    }
}
