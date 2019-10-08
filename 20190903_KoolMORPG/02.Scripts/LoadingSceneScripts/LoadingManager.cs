using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class LoadingManager : MonoBehaviour
{
    public Text dotText;

    private bool isAllWorkDone =false;
    private int connectionCheckCount = 0; // 연결 체크 횟수
    private const int MAX_CONNECTION_CHECK_COUNT = 5;

    private void Start()
    {
#if RELEASE_MODE
        CheckConnectionToServer();
        ShowDotTextWhileLoading();
#elif DEVELOP_MODE
        SetAllData();
#endif
    }

    // 서버와 연결이 되어있는지 체크한다.
    public void CheckConnectionToServer()
    {
        StartCoroutine(ICheckConnectionToServer());
    }

    private IEnumerator ICheckConnectionToServer()
    {
        while (true)
        {
            if(connectionCheckCount <= MAX_CONNECTION_CHECK_COUNT)
            {
                StartCoroutine(NetworkManager.instance.ICheckConnectionToServer()); // 서버와 연결 시도
                ++connectionCheckCount;
                yield return new WaitForSeconds(1f);
            }
            else
            {
                Debug.Log("연결 실패 팝업 오픈!");
                OpenConnectFailPopup(true);
                
                yield return null;
                break;
            }      

            if (NetworkManager.instance.isConnected)
            {
                SetAllData();
                yield return null;
                SceneMoveManager.LoadScene("LoginScene");
                break;
            }
            Debug.Log("while");
        }
    }
    // FIXED (장현명) : 쓸모없는부분같음...
    // 모든 데이터들 설정하기.
    public void SetAllData()
    {
        isAllWorkDone = true;
    }

    // Loading 하는 동안 Loading Text에 점 표시가 바뀌는것을 보여준다.
    public void ShowDotTextWhileLoading()
    {
        StartCoroutine(IShowDotTextWhileLoading());
    }

    private IEnumerator IShowDotTextWhileLoading()
    {
        dotText.text = ".";

        while (true)
        {
            if (isAllWorkDone)
            {
                break;
            } else
            {
                if (dotText.text == "...")
                {
                    dotText.text = "";
                }
                dotText.text += ".";
                yield return new WaitForSeconds(1f);
            }
        }
        yield return null;
    }
    
    ////////////// Popop /////////////

    // 연결 실패 팝업 On/Off
    public void OpenConnectFailPopup(bool isOpen)
    {
        if (isOpen)
        {
            PopupManager.instance.OpenConnectionFailPopup(true);
        } else
        {
            PopupManager.instance.OpenConnectionFailPopup(false);
        }
    }

    // 서버에 다시 연결하는 버튼 클릭
    public void OnClick_ReconnectToServerBtn()
    {
        connectionCheckCount = 0;
        CheckConnectionToServer();
        OpenConnectFailPopup(false);
    }
}