using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//network socket 
using System.Net;
using System.Net.Sockets;
using System.Text; // ?

public class SimpleServer : MonoBehaviour {

    static SimpleServer singleton;

    private Socket m_Socket;
    ArrayList m_Connections = new ArrayList();
    ArrayList m_Buffer = new ArrayList();
    ArrayList m_ByteBuffer = new ArrayList();


    private void Awake()
    {
        //소켓을 생성합니다.
        m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, SimpleClient.kPort);
        //Bind 함수를 호출해서 이제부터 운영체제의 네트워크 시스템과 연계합니다.
        m_Socket.Bind(ipLocal);
        //연결 요청을 기다리기 시작합니다.
        m_Socket.Listen(100);
        singleton = this;
    }

    // Use this for initialization
    void Start () {
        Debug.Log("Start listening...");
	}

    private void OnApplicationQuit()
    {
        Cleanup();
    }
    void Cleanup()
    {
        if(m_Socket != null)
        {
            m_Socket.Close();
        }
        m_Socket = null;
        foreach(Socket con in m_Connections)
        {
            con.Close();
        }
        m_Connections.Clear();
    }

    // Update is called once per frame
    void Update () {
        //연결된 리스트입니다.
        ArrayList listenList = new ArrayList();
        listenList.Add(m_Socket);
        Socket.Select(listenList, null, null, 1000);
        for(int i=0;i<listenList.Count; i++)
        {
            Socket newSocket = ((Socket)listenList[i]).Accept();
            m_Connections.Add(newSocket);
            m_ByteBuffer.Add(new ArrayList());
            Debug.Log("Did connect");
        }

        //연결된 호스트들로부터 데이터를 읽습니다.
        if(m_Connections.Count != 0)
        {
            ArrayList connections = new ArrayList(m_Connections);
            Socket.Select(connections, null, null, 1000);
            //모든 연결된 소켓들로부터 들어온 데이터를 가져옵니다.
            foreach(Socket socket in connections)
            {
                byte[] receivedbytes = new byte[512];
                ArrayList buffer = (ArrayList)m_ByteBuffer[m_Connections.IndexOf(socket)];
                int read = socket.Receive(receivedbytes);
                for(int i = 0; i < read; i++)
                {
                    buffer.Add(receivedbytes[i]);
                }
                while(true && buffer.Count > 0)
                {
                    int length = (byte)buffer[0];
                    if(length < buffer.Count)
                    {
                        ArrayList thismsgBytes = new ArrayList(buffer);
                        thismsgBytes.RemoveRange(length + 1, thismsgBytes.Count - (length + 1));
                        thismsgBytes.RemoveRange(0, 1);
                        if (thismsgBytes.Count != length)
                            Debug.Log("Bug");
                        buffer.RemoveRange(0, length + 1);
                        byte[] readbytes = (byte[])thismsgBytes.ToArray(typeof(byte));
                        MessageData readMsg = MessageData.FromByteArray(readbytes);
                        m_Buffer.Add(readMsg);
                        Debug.Log(System.String.Format("Message {0}: {1}, {2}", readMsg.stringData, readMsg.mousex, readMsg.mousey));
                        if(singleton != this)
                        {
                            Debug.LogError("Bug");
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
	}
    static public MessageData PopMessage()
    {
        if(singleton.m_Buffer.Count == 0)
        {
            return null;
        }
        else
        {
            MessageData readMsg = (MessageData)singleton.m_Buffer[0];
            Debug.Log(System.String.Format("Message {0} : {1}, {2}", readMsg.stringData, readMsg.mousex, readMsg.mousey));
            return readMsg;
        }
    }
}
