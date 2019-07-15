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
    public GameObject statusBarUI;
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
    public GameObject result_NewHightScoreObj;
    public Text result_ScoreText;
    public Text result_CoinText;
    


    // ***********  FUNCTION  ***********

    // ----------- In menu screen ----------
    // Tap to play 버튼을 누름.
    public void OnClick_TapToPlay()
    {
        BallGameManager.instance.StartGame(BallGameManager.GameStartStatus.newStart);

        menuUI.SetActive(false);
        statusBarUI.SetActive(false);
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


    public void ResultScreen(bool openFlag)
    {
        if (openFlag)
        {
            resultScreen.SetActive(true);
            // 스코어 최고기록을 갱신하면
            if (BallGameManager.instance.user.scoreFromTheGame > BallGameManager.instance.user.topScore)
                result_NewHightScoreObj.SetActive(true); // 최고기록 갱신 화면을 UI에 보여준다.
            else // 최고기록 갱신하지 못하면
                result_NewHightScoreObj.SetActive(false); // 최고기록 갱신 화면을 숨긴다.

            result_ScoreText.text = BallGameManager.instance.user.scoreFromTheGame.ToString(); // 현재 기록을 UI에 보여줌.
            result_CoinText.text = BallGameManager.instance.user.coinFromTheGame.ToString(); // 현재 얻은 코인을 UI에 보여줌.
            BallGameManager.instance.UpdateUserInfoAfterGameOver(); // 게임이 끝난 후 정보 업데이트
<<<<<<< HEAD
            UpdateStatusBar(); // 상태 바 업데이트
=======
            UpdateStateBar(); // 상태 바 업데이트
>>>>>>> 59503538d7d101cd2babf6c84eba28dbf1c4b6f5
        }
        else
        {
            resultScreen.SetActive(false);
        }
    }

    // ------------- In Result Screen ---------------
    // Home 버튼을 누름
    public void OnClick_HomeBtn()
    {
        resultScreen.SetActive(false);
        gamePlayUI.SetActive(false);
<<<<<<< HEAD
        statusBarUI.SetActive(true);
        BallGameManager.instance.ResetMap(); // 맵 리셋
=======
        stateBarUI.SetActive(true);
        BallGameManager.instance.ResetMap();
>>>>>>> 59503538d7d101cd2babf6c84eba28dbf1c4b6f5
    }

    // Play 버튼을 누름
    public void OnClick_PlayBtn()
    {
        OnClick_TapToPlay();
        BallGameManager.instance.ResetMap(); // 맵 리셋

    }
    

}

// GamePlayUI
public partial class UIManager
{
    // --------- In GamePlay UI --------
    [Header("In Game Play Screen")]
    // game play screen
    public GameObject pauseScreen;
<<<<<<< HEAD
=======

    public Text gamePlay_scoreText;
    public Text gamePlay_coinText;
   
>>>>>>> 59503538d7d101cd2babf6c84eba28dbf1c4b6f5

    public Text gamePlay_scoreText;
    public Text gamePlay_coinText;
    [System.Serializable]
    public struct GamePlay_ItemsPanel{
        public GameObject itemPanel;
        public Image gageImage;
    }
    public GamePlay_ItemsPanel[] gamePlay_itemPanel;

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
    

    // 게임 화면 업데이트
    public void UpdateGameUI_InGamePlayScreen(GameEventOccurStatus status)
    {
        switch (status)
        {
            case GameEventOccurStatus.GetCoin: // 코인을 얻었을 경우, Coin UI를 업데이트 한다. 
                gamePlay_coinText.text = BallGameManager.instance.user.coinFromTheGame.ToString();
                break;

            case GameEventOccurStatus.GetItem: // 아이템을 얻었을 경우, 아이템 UI를 업데이트 한다.

                // TODO(1) : 아이템을 얻었을 경우 UI에 보여준다.
                
                break;

            case GameEventOccurStatus.UpdateUI: // 모든 상태를 업데이트 한다.
                gamePlay_coinText.text = BallGameManager.instance.user.coinFromTheGame.ToString();
                gamePlay_scoreText.text = BallGameManager.instance.user.scoreFromTheGame.ToString();
                break;
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
    public Image continue_revivalTimerImg;
    public Text continue_TimerText;
    public Text continue_RevivalCountText;
    public Text continue_NeededForRevivalItemCountText;




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
        if (openFlag)
        {
            continuePopup.SetActive(true);
<<<<<<< HEAD
            BallGameManager.instance.ActivateRevivalTimer(ActivateStatus.Start, BallGameManager.instance.revivalTimer); // 30초 정도 타이머 시작.
=======
            BallGameManager.instance.ActivateRevivalTimer(ActivateState.Start, BallGameManager.instance.revivalTimer); // 30초 정도 타이머 시작.
>>>>>>> 59503538d7d101cd2babf6c84eba28dbf1c4b6f5
            continue_NeededForRevivalItemCountText.text = BallGameManager.instance.user.numNeededForRevivalItem.ToString();  // 부활에 필요한 부활 아이템 개수를 UI에 보이기
        }
        else
        {
            
            BallGameManager.instance.SaveUserInfoToDB(); // 데이터 베이스에 정보 저장 및 서버와 통신하기
<<<<<<< HEAD
            BallGameManager.instance.ActivateRevivalTimer(ActivateStatus.Stop); // 타이머 종료
=======
            BallGameManager.instance.ActivateRevivalTimer(ActivateState.Stop); // 타이머 종료
>>>>>>> 59503538d7d101cd2babf6c84eba28dbf1c4b6f5
            continuePopup.SetActive(false);
            menuUI.SetActive(true);
            ResultScreen(true);
        }
    }

    // -------------- In Continue popup ---------------
    // 부활 아이템 버튼 클릭 시
    public void OnClick_UseRevivalBtn_InContinuePopup()
    {
        continue_RevivalCountText.text = BallGameManager.instance.user.revivalItem.ToString(); // revival item 개수 화면에 보여줌.

        // 가지고 있는 부활 아이템의 개수가 부활 아이템 개수에 비해 모자를 경우
        if (BallGameManager.instance.user.revivalItem < BallGameManager.instance.user.numNeededForRevivalItem)
        {
            purchaseAlarmPopup.SetActive(true); // 구입 불가 화면을 띄움.
        }
        else{
            // 계속해서 게임 시작 
<<<<<<< HEAD
            BallGameManager.instance.StartGame(BallGameManager.GameStartStatus.continueStart); // 이어서 게임 시작
            BallGameManager.instance.ActivateRevivalTimer(ActivateStatus.Stop); //Timer 종료
=======
            BallGameManager.instance.StartGame(BallGameManager.GameStartState.continueStart); // 이어서 게임 시작
            BallGameManager.instance.ActivateRevivalTimer(ActivateState.Stop); //Timer 종료
>>>>>>> 59503538d7d101cd2babf6c84eba28dbf1c4b6f5

            // 화면 전환
            continuePopup.SetActive(false);
            menuUI.SetActive(true);
            ResultScreen(true);
        }
    }


    public void OnClick_WatchAdvertismentBtn_InContinuePopup()
    {
        // TODO : 30초 광고영상 넣기
    }
}

<<<<<<< HEAD
// Status Bar UI
public partial class UIManager
{
    [Header("Status Bar")]
    public Text statusBar_ScoreText;
    public Text statusBar_ProtectedItemText;
    public Text statusBar_RevivalItemText;
    public Text statusBar_CoinText;


    // **************** FUNCTION ****************
    // ------------- In the Status Bar -------------
    // 상태 바 업데이트
    public void UpdateStatusBar()
    {
        statusBar_ScoreText.text = BallGameManager.instance.user.topScore.ToString();
        statusBar_ProtectedItemText.text = BallGameManager.instance.user.protectedItem.ToString();
        statusBar_RevivalItemText.text = BallGameManager.instance.user.revivalItem.ToString();
        statusBar_CoinText.text = BallGameManager.instance.user.coin.ToString();
=======
// State Bar UI
public partial class UIManager
{
    [Header("State Bar")]
    public Text stateBar_ScoreText;
    public Text stateBar_ProtectedItemText;
    public Text stateBar_RevivalItemText;
    public Text stateBar_CoinText;


    // **************** FUNCTION ****************
    // ------------- In the State Bar -------------
    // 상태 바 업데이트
    public void UpdateStateBar()
    {
        stateBar_ScoreText.text = BallGameManager.instance.user.topScore.ToString();
        stateBar_ProtectedItemText.text = BallGameManager.instance.user.protectedItem.ToString();
        stateBar_RevivalItemText.text = BallGameManager.instance.user.revivalItem.ToString();
        stateBar_CoinText.text = BallGameManager.instance.user.coin.ToString();
>>>>>>> 59503538d7d101cd2babf6c84eba28dbf1c4b6f5
    }


}