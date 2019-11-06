using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopItemDisplay : MonoBehaviour
{
    public ShopItem card;

    public Text titleText;
    public Image itemImage;
    public Image costImage;
    public Text costText;

    private ShopPopupManager shopPopupManager;

    void Start()
    {
        shopPopupManager = gameObject.GetComponentInParent<ShopPopupManager>();
        UpdateUI();
    }
    
    /// <summary>   
    /// 카드의 UI를 업데이트한다.
    /// </summary>
    public void UpdateUI()
    {
        titleText.text = card.title;
        itemImage.sprite = card.itemIcon;
        costImage.sprite = card.costIcon;
        costText.text = card.cost.ToString();
    }
    
    /// <summary>
    /// 구입 버튼을 누를 경우
    /// </summary>
    public void OnClick_PurchaseBtn()
    {
        Debug.Log("clicked!");
        shopPopupManager.currentChoicedItem = card;
        shopPopupManager.OpenReconfirmPopup(true);
    }
}
