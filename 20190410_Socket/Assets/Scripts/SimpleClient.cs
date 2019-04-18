using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
public class SimpleClient : MonoBehaviour {
    public string m_IPAdress = "127.0.0.1";
    public const int kPort = 10253;
    private static SimpleClient singleton;
    private Socket m_Socket;

    private void Awake()
    {
        // 소켓을 생성합니다.
        m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        System.Net.IPAddress remoteIPAddress = System.Net.IPAddress.Parse(m_IPAdress);
        System.Net.IPEndPoint remoteEndPoint = new System.Net.IPEndPoint(remoteIPAddress, kPort);
        singleton = this;
        //서버에 연결 요청을 합니다.
        m_Socket.Connect(remoteEndPoint);
        Debug.Log("Connecting");
    }

    private void OnApplicationQuit()
    {
        m_Socket.Close();
        m_Socket = null;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //마우스 왼쪽 클릭 때마다 현재 마우스 좌표와 Hello 문자열을 서버에게 보냅니다.
        if (Input.GetMouseButtonUp(0))
        {
            MessageData newmsg = new MessageData();
            newmsg.stringData = "hello";
            newmsg.mousex = Input.mousePosition.x;
            newmsg.mousey = Input.mousePosition.y;
            newmsg.type = 0;
            SimpleClient.Send(newmsg);
        }
	}
    static public void Send(MessageData msgData)
    {
        if(singleton.m_Socket == null)
        {
            return;
        }
        byte[] sendData = MessageData.ToByteArray(msgData);
        byte[] prefix = new byte[1];
        prefix[0] = (byte)sendData.Length;
        singleton.m_Socket.Send(prefix);
        singleton.m_Socket.Send(sendData);
        //Log
        Debug.Log(msgData.stringData + " " + msgData.mousex.ToString() + " " + msgData.mousey.ToString());
    }
}
