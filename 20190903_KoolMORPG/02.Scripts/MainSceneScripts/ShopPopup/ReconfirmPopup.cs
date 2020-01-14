using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReconfirmPopup : MonoBehaviour
{
    public string popupTitle;                  // 팝업창에 보여줄 타이틀
    
    [Header("Shop popup")]
    public ShopPopupManager shopPopupManager;  // 팝업 매니저

    [Header("ReconfirmPopup")]
    public Text itemPackName;                  // 아이템 팩의 이름

    

    /// <summary>
    /// 들어오는 값의 종류에 따라서 재확인창의 값을 준다.
    /// </summary>
    public void UpdateUI(string title = "")
    {
        popupTitle = shopPopupManager.currentChoicedItem.title;
        itemPackName.text = popupTitle + "을(를)구매 하시겠습니까?";
    }

    /// <summary>
    /// 재확인 버튼 클릭 시 호출
    /// </summary>
    public void OnClick_ConfirmBtn()
    {
        if (shopPopupManager.currentChoicedItem.shopItemType == EShopItemType.crystal) // 현금으로 결제하는 용도면
        {
            // TODO (장현명) : GPGS 처리해줘야 함
            shopPopupManager.PurchaseCrystal();
        }
        else // crystal로 결제하는 용도면
        {
            // 가격이 부족할 경우
            if (PlayerInformation.userData.crystal < shopPopupManager.currentChoicedItem.cost)
            {
                gameObject.SetActive(false);
                shopPopupManager.OpenSuggestionPopup(true);
                return;
            }
            shopPopupManager.PurchaseItem();
        }
        shopPopupManager.OpenReconfirmPopup(false);
    }
}
