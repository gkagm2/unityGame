using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [Header("Screen")]
    // screen
    public GameObject meScreen;
    public GameObject shopScreen;

    [Header("In Me Screen")]
    public GameObject ballItemScreen;
    public GameObject ballBtn;

    [Header("In ShopScreen")]
    // shop screen
    public GameObject boostsScreen;
    public GameObject storeScreen;

    public GameObject boostsBtn;
    public GameObject storeBtn;

    [Header("Popup")]
    // popup
    public GameObject purchaseAlarmPopup;






    // ----------- In menu screen ----------

    // Rank 버튼을 누름.
    public void OnClick_RankBtn()
    {
        // TODO : 구글 리더보드.
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
