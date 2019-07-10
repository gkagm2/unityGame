using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public partial class UIManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject menuUI;
    public GameObject stateBarUI;
    public GameObject gamePlayUI;
}

// MenuUI
public partial class UIManager : MonoBehaviour {

    


    // -------- In Menu UI ----------
    [Header("In Menu Screen")]
    // screen
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

    [Header("Popup")]
    // popup
    public GameObject purchaseAlarmPopup;


    

    // ***********  FUNCTION  ***********

    // ----------- In menu screen ----------
    // Tap to play 버튼을 누름.
    public void OnClick_TapToPlay()
    {
        BallGameManager.instance.GameStart();

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



    // ------------ Purchase alarm popup -------------

    // 구매 알람 팝업
    public void PurchaseAlarmPopup(bool openFlag)
    {
        if (openFlag)
            purchaseAlarmPopup.SetActive(true);
        else
            purchaseAlarmPopup.SetActive(false);
    }


    
}

// GamePlayUI
public partial class UIManager
{
    // --------- In GamePlay UI --------
    [Header("In Game Play Screen")]
    // game play screen
    public GameObject pauseScreen;
    


    // -------------- FUNCTION -----------
    // Pause 버튼을 눌렀을 경우
    public void OnClick_PauseBtn(bool openFlag)
    {
        if (openFlag)
        {
            pauseScreen.SetActive(true);
            // TODO : puase game
        }
        else
        {
            pauseScreen.SetActive(false);
        }
    }
}