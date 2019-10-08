using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 아이템 판매 확인 팝업
/// </summary>
public class ItemSaleConfirmPopup : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public DetailItemCardPopup detailItemCardPopup;
    public Text nameText;
    public Text goldText;

    /// <summary>
    /// UI 화면을 업데이트한다.
    /// </summary>
    public void UpdateUI()
    {
        nameText.text = detailItemCardPopup.inventoryItemCardInDetailPanel.card.name;
        goldText.text = detailItemCardPopup.inventoryItemCardInDetailPanel.card.gold.ToString();
    }

    /// <summary>
    /// 아이템 판매 팝업 창을 닫는 버튼 클릭 시 호출
    /// </summary>
    public void OnClick_CloseBtn()
    {
        // 3D 캐릭터가 팝업을 덮어서 팝업을 PopupCanvas에 넣음.
        PopupManager.instance.OpenItemSaleConfirmPopup(false); // 아이템 세일 확인 팝업 닫기
    }

    /// <summary>
    /// (판매)확인 버튼 클릭 시 호출
    /// </summary>
    public void OnClick_SaleBtn()
    {
        // TOOD (장현명) : 아이템이 null일 때 처리해야 함.
        // 삭제하기 전에 사용중인 아이템인지 확인한다.
        
        if (detailItemCardPopup.inventoryItemCardInDetailPanel.card.id == PlayerInformation.inventory.currentEquippedArmorOrNull.id ||
            detailItemCardPopup.inventoryItemCardInDetailPanel.card.id == PlayerInformation.inventory.currentEquippedHelmetOrNull.id ||
            detailItemCardPopup.inventoryItemCardInDetailPanel.card.id == PlayerInformation.inventory.currentEquippedShoesOrNull.id ||
            detailItemCardPopup.inventoryItemCardInDetailPanel.card.id == PlayerInformation.inventory.currentEquippedWeaponOrNull.id)
        {
            PopupManager.instance.OpenAlarmPopup("사용중인 아이템입니다. 장비 착용을 해제하세요.");
            PopupManager.instance.OpenItemSaleConfirmPopup(false); // 아이템 세일 확인 팝업 닫기
            detailItemCardPopup.OpenDetailItemCardPopup(false);
        }
        else
        {
            SaleItem();
        }
    }

    public void SaleItem()
    {
        PopupManager.instance.OpenItemSaleConfirmPopup(false); // 아이템 세일 확인 팝업 닫기
        inventoryManager.SaleEquipmentItem(detailItemCardPopup.inventoryItemCardInDetailPanel.card); // 아이템을 판매한다.
        detailItemCardPopup.OpenDetailItemCardPopup(false);
    }
}
