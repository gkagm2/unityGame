using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public partial class UIManager : MonoBehaviour
{

    #region Singleton
    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    [Header("UI")]
    public GameObject menuUI;
    public GameObject stateBarUI;
    public GameObject gamePlayUI;



    





}

// MenuUI
public partial class UIManager : MonoBehaviour {

    


    // -------- In Menu UI ----------
    [Header("In Menu Screen")]
    // menu screen
    public GameObject meScreen;
    public GameObject shopScreen;

    [Header("In Me Screen")]
    public GameObject ballItemScreen;
    public GameObject ballBtn;

    [Header("In Shop Screen")]
    // shop screen
    public GameObject boostsScreen;
    public GameObject storeScreen;

    public GameObject boostsBtn;
    public GameObject storeBtn;


    [Header("In Result Screen")]
    // result screen
    public GameObject resultScreen;


    // ***********  FUNCTION  ***********

    // ----------- In menu screen ----------
    // Tap to play 버튼을 누름.
    public void OnClick_TapToPlay()
    {
        BallGameManager.instance.StartGame(BallGameManager.GameStartState.newStart);

        menuUI.SetActive(false);
        stateBarUI.SetActive(false);
        gamePlayUI.SetActive(true);
    }

    // Rank 버튼을 누름.
    public void OnClick_RankBtn(bool openFlag)
    {
        // TODO : 구글 리더보드.
        if (openFlag)
            Debug.Log("open Rank screen");
        else
            Debug.Log("close Rank screen");
    }

    // Me 버튼을 누름.
    public void OnClick_MeBtn(bool openFlag)
    {
        if (openFlag)
        {
            OnClick_BallBtn();
            meScreen.SetActive(true);
        }
        else
            meScreen.SetActive(false);
    }

    // Shop 버튼을 누름.
    public void OnClick_ShopBtn(bool openFlag)
    {
        if (openFlag)
        {
            OnClick_BoostsBtn(); // Boosts 화면으로 전환.
            shopScreen.SetActive(true);
        }
        else
            shopScreen.SetActive(false);
    }


    // ------------- In Me Screen --------------
    // Ball 버튼을 누름
    public void OnClick_BallBtn()
    {
        ballItemScreen.SetActive(true);
        ballBtn.GetComponent<Image>().color = Color.green;
    }


    // ------------- In Shop Screen --------------
    // Boosts 버튼을 누름
    public void OnClick_BoostsBtn()
    {
        boostsScreen.SetActive(true);
        storeScreen.SetActive(false);
        boostsBtn.GetComponent<Image>().color = Color.green;
        storeBtn.GetComponent<Image>().color = Color.white;
    }
    
    // Store 버튼을 누름
    public void OnClick_StoreBtn()
    {
        storeScreen.SetActive(true);
        boostsScreen.SetActive(false);
        storeBtn.GetComponent<Image>().color = Color.green;
        boostsBtn.GetComponent<Image>().color = Color.white;
    }

    // ------------- In Result Screen ---------------
    // Home 버튼을 누름
    public void OnClick_HomeBtn()
    {
        resultScreen.SetActive(false);
        BallGameManager.instance.ResetMap();
    }

    // Play 버튼을 누름
    public void OnClick_PlayBtn()
    {
        OnClick_TapToPlay();
        BallGameManager.instance.ResetMap();

    }
    

}

// GamePlayUI
public partial class UIManager
{
    // --------- In GamePlay UI --------
    [Header("In Game Play Screen")]
    // game play screen
    public GameObject pauseScreen;
   


    // ***********  FUNCTION  ***********
    // Pause 버튼을 눌렀을 경우
    public void OnClick_PauseBtn(bool openFlag)
    {
        if (openFlag)
        {
            pauseScreen.SetActive(true);
            BallGameManager.instance.PauseGame();
        }
        else
        {
            pauseScreen.SetActive(false);
            BallGameManager.instance.PauseGame();
        }
    }

}

// Popup
public partial class UIManager
{
    // popup
    [Header("Purchase Alarm Popup")]
    public GameObject purchaseAlarmPopup;

    [Header("Continue Popup")]
    public GameObject continuePopup;





    // ****************** FUNCTION *******************

    // -----------------------------------------------
    // ------------ Purchase alarm popup -------------

    // 구매 알람 팝업
    public void PurchaseAlarmPopup(bool openFlag)
    {
        if (openFlag)
            purchaseAlarmPopup.SetActive(true);
        else
            purchaseAlarmPopup.SetActive(false);
    }


    // -----------------------------------------------
    // --------------- Continue popup ----------------

    // 게임 이어서 계속하기 팝업
    public void ContinuePopup(bool openFlag)
    {
        Debug.Log("게임 이어서 계속하기 팝업 시작");
        if (openFlag)
        {
            continuePopup.SetActive(true);
            BallGameManager.instance.StartRevivalTimer(30); // 30초 정도 타이머 시작.
            Debug.Log("팝업 시작!!");
        }
        else
        {
            // TODO : 데이터 베이스에 정보 저장 및 서버와 통신하기

            BallGameManager.instance.StopRevivalTimer(); // 타이머 종료
            Debug.Log("STop!!!!!!!!!!!");
            continuePopup.SetActive(false);
            menuUI.SetActive(true);
            resultScreen.SetActive(true);
        }
    }

    // -------------- In Continue popup ---------------
    public void OnClick_UseRevivalBtn_InContinuePopup()
    {

        // 가지고 있는 부활 아이템의 개수가 부활 아이템 개수에 비해 모자를 경우
        if (BallGameManager.instance.user.revivalItem < BallGameManager.instance.numNeededForRevivalItem)
        {
            purchaseAlarmPopup.SetActive(true); // 구입 불가 화면을 띄움.
        }
        else{
            // 계속해서 게임 시작 
            BallGameManager.instance.StartGame(BallGameManager.GameStartState.continueStart);

            BallGameManager.instance.StopRevivalTimer(); //Timer 종료

            // 화면 전환
            continuePopup.SetActive(false);
            menuUI.SetActive(true);
            resultScreen.SetActive(true);
        }
    }


    public void OnClick_WatchAdvertismentBtn_InContinuePopup()
    {
        // TODO : 30초 광고영상 넣기
    }
}