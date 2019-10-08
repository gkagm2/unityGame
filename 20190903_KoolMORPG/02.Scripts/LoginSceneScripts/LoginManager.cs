using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    [Header("Warning popup")]
    public GameObject warningPopup;


    [Header("Create user name popup")]
    public InputField nameInputField;
    public GameObject createUserNamePopup;
    
    // 구글 아이디로 로그인 버튼
    public void OnClick_GoogleLoginBtn()
    {
        // TODO : GPGS service 붙여야 한다.
        if (PlayerPrefs.HasKey("name"))
        {
            Debug.Log("내부에 name이 있습니다.!");
            // TODO (장현명) : 데이터가 안불러와질것임 StartCoroutine으로 처리해야 함.
            //PlayerInformation.LoadData(ESaveLoadMode.server); // 정보 불러오기.
            Debug.Log("플레이어 데이터를 Server에서 불러옵니다.");
            Debug.Log("캐릭터 선택 씬으로 이동합니다.");
            SceneMoveManager.LoadScene("CharacterChoiceScene");
        }
        else
        {
            Debug.Log("내부에 name이 없습니다.");
            Debug.Log("PlayerInformation에 로그인 모드를 GoogleLoginMode : true로 바꿉니다.");
            PlayerInformation.eUserCreateMode = EUserCreateMode.withGPGSId;
            if (NetworkManager.instance != null)
            {
                NetworkManager.instance.isGoogleLoginMode = true;
                OpenCreateUserNamePopup(true);
            }
            else
            {
                Debug.LogWarning("NetworkManager가 없습니다. 화면 전환 불가능");
            }

            // TODO (장현명) : GPGS Service를 연동하여  ID를 받아온다.
            //Get GPGS ID from google server. and get GPGS ID from our server
        }
        
    }

    // 로그인 없이 게임 시작하기 버튼 
    public void OnClick_JustGameStartBtn()
    {
        if (PlayerPrefs.HasKey("name"))
        {
            Debug.Log("내부에 name이 있습니다.");
            // 플레이어 데이터 불러오기.

            // TODO (장현명) : 데이터가 안불러와질것임 StartCoroutine으로 처리해야 함.
            //PlayerInformation.LoadData(ESaveLoadMode.application);
            Debug.Log("플레이어 데이터를 Application에서 불러옵니다.");
            Debug.Log("캐릭터 선택 씬으로 이동합니다.");
            SceneMoveManager.LoadScene("CharacterChoiceScene");
        }
        else
        {
            Debug.Log("내부에 name이 없습니다.");
            Debug.Log("PlayerInformation에 로그인 모드를 GoogleLoginMode : false로 바꿉니다.");
            PlayerInformation.eUserCreateMode = EUserCreateMode.withoutGPGSId;
            if(NetworkManager.instance == null)
            {
                Debug.LogWarning("Network Manager가 없음. GoogleLoginMode를 false로 바꿀 수 없다");
            }
            else
            {
                NetworkManager.instance.isGoogleLoginMode = false;
            }
            OpenWarningPopup(true);
        }
    }

    /////////////////// Warning Popup ////////////////////

    // 경고 팝업창 On/Off.
    public void OpenWarningPopup(bool isOpen)
    {
        if (isOpen)
        {
            warningPopup.SetActive(true);
        }
        else
        {
            warningPopup.SetActive(false);
        }
    }

    // 경고 팝업창 시작 버튼 클릭
    public void OnClick_WarningOkBtn()
    {
        OpenWarningPopup(false);
        OpenCreateUserNamePopup(true);
    }


    /////////////// Create User Name Popup //////////////

    // 유저 이름 생성 팝업창 On/Off
    public void OpenCreateUserNamePopup(bool isOpen)
    {
        if (isOpen)
        {
            createUserNamePopup.SetActive(true);
        }
        else
        {
            createUserNamePopup.SetActive(false);
        }
    }

    // 유저 이름 생성 버튼 클릭
    public void OnClick_CreateUserNameBtn()
    {
        if (nameInputField.text == "")
        {
            Debug.Log("빈칸입니다.");
            return;
        }

        Debug.Log("nameInputField에서 " + nameInputField.text + "값을 넣었습니다.");
        if (NetworkManager.instance != null)
        {
            StartCoroutine(NetworkManager.instance.ICheckUserNameExist(nameInputField.text));
            nameInputField.text = "";
        }
        else
        {
            Debug.LogError("NetworkManager의 reference가 setting되어있지 않습니다. LoadingScene에서 시작하십시오");
        }
    }

    /// <summary>
    /// 이어하기 버튼 클릭 시 호출
    /// </summary>
    public void OnClick_ContinueGameStartBtn()
    {
        if (PlayerPrefs.HasKey("Name"))
        {
            PlayerInformation.userData.name = PlayerPrefs.GetString("Name");
            SceneMoveManager.LoadScene("CharacterChoiceScene");
        }
        else
        {
            PopupManager.instance.OpenAlarmPopup("내부에 저장된 데이터가 없습니다.");
        }
    }
}