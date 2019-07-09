using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [Header("Screen")]
    // screen
    public GameObject meScreen;
    public GameObject shopScreen;

    [Header("Popup")]
    // popup
    public GameObject purchaseAlarmPopup;






    // ----------- menu ----------

    // Rank 버튼을 누름.
    public void OnClick_RankBtn()
    {
        // TODO : 구글 리더보드.
    }

    // Me 버튼을 누름.
    public void OnClick_MeBtn(bool openFlag)
    {
        if (openFlag)
            meScreen.SetActive(true);
        else
            meScreen.SetActive(false);
    }

    // Shop 버튼을 누름.
    public void OnClick_ShopBtn(bool openFlag)
    {
        if (openFlag)
            shopScreen.SetActive(true);
        else
            shopScreen.SetActive(false);
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
