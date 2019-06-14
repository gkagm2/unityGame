using System.Collections;
using UnityEngine;

using System.Net;
using System.Net.Sockets;
using System.Threading;


public class TransportTCP : MonoBehaviour {


    //
    // 소켓 접속 관련.
    //

    // 리스닝 소켓.
    private Socket m_listner = null;

    // 클라이언트와의 접속용 소켓.
    private Socket m_socket = null;

    // 송신 버퍼.
    private PacketQueue m_sendQueue;

    // 수신 버퍼.
    private PacketQueue m_recvQueue;

    // 서버 플래그.
    private bool m_isServer = false;

    // 접속 플래그.
    private bool m_isConnected = false;

    //
    // 이벤트 관련 멤버 함수.
    //

    // 이벤트 통지 델리게이트.
    public delegate void EventHandler(NetEventState state);

    private EventHandler m_handler;

    //
    // 스레드 관련 멤버 변수.
    //

    // 스레드 실행 플레그.
    protected bool m_threadLoop = false;

    protected Thread m_thread = null;

    // 최대 전송 단위(maximum transmission unit, MTU)
    private static int s_mtu = 1400;

    private void Start()
    {
        // 송수신 버퍼를 작성합니다.
        m_sendQueue = new PacketQueue();
        m_recvQueue = new PacketQueue();


    }
    private void Update()
    {
    }







}
