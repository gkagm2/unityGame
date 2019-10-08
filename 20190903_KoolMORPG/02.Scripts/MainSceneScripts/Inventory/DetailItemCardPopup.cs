using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DetailItemCardPopup : MonoBehaviour
{
    public GameObject detailItemCardPopup;
    [HideInInspector]
    public GameObject choicedItemCardObj;          // (사용하지 말자)아이템 카드를 클릭했을 때 아이템 카드의 정보가 담기는 오브젝트

    [Header("Equip button")]
    public GameObject unEquipButton;                   // 장착 해제 버튼
    public GameObject equipButton;

    [Header("Item card display")]
    public InventoryItemDisplay inventoryItemCardInDetailPanel;    // detail item card popup 안에 있는 inventory item card
    public InventoryManager inventoryManager;

    public Text attackPowerText;
    public Text defencePowerText;
    public Text attackSpeedText;

    [Header("Item sale confirm popup")]
    public ItemSaleConfirmPopup itemSaleConfirmPopup;           // 아이템 판매 확인 팝업
    
    //////////// detail item card popup /////////////
    public void OpenDetailItemCardPopup(bool isOpen)
    {
        if (isOpen)
        {
            detailItemCardPopup.SetActive(true);
            UpdateUI();
        }
        else
        {
            detailItemCardPopup.SetActive(false);
        }
    }

    /// <summary>
    /// UI를 업데이트할 때 호출
    /// </summary>
    public void UpdateUI()
    {
        // scroll view 패널에서 선택한 아이템의 카드 정보를 detail화면의 detailItemCard에 복사한다.
        inventoryItemCardInDetailPanel.card.CopyData(choicedItemCardObj.GetComponent<InventoryItemDisplay>().card);
        inventoryItemCardInDetailPanel.UpdateUI();

        attackPowerText.text = inventoryItemCardInDetailPanel.card.atk.ToString();
        defencePowerText.text = inventoryItemCardInDetailPanel.card.def.ToString();
        attackSpeedText.text = inventoryItemCardInDetailPanel.card.attackSpeed.ToString();

        ChangeEquipOrUnEquipButton();
    }

    /// <summary>
    /// 장착 버튼 클릭 시 호출
    /// </summary>
    public void OnClick_EquipBtn()
    {
        StartCoroutine(IOnClick_EquipBtn());
    }
    private IEnumerator IOnClick_EquipBtn()
    {
        switch (inventoryItemCardInDetailPanel.card.type)
        {
            case EEquipmentItemType.armor:
                PlayerInformation.inventory.currentEquippedArmorOrNull.CopyData(inventoryItemCardInDetailPanel.card);
                inventoryManager.armorDisplay.card.CopyData(PlayerInformation.inventory.currentEquippedArmorOrNull);
                break;
            case EEquipmentItemType.helmet:
                PlayerInformation.inventory.currentEquippedHelmetOrNull.CopyData(inventoryItemCardInDetailPanel.card);
                inventoryManager.helmetDisplay.card.CopyData(PlayerInformation.inventory.currentEquippedHelmetOrNull);
                break;
            case EEquipmentItemType.shoes:
                PlayerInformation.inventory.currentEquippedShoesOrNull.CopyData(inventoryItemCardInDetailPanel.card.GetCopiedData());
                inventoryManager.shoesDisplay.card.CopyData(PlayerInformation.inventory.currentEquippedShoesOrNull);
                break;
            case EEquipmentItemType.weapon:
                PlayerInformation.inventory.currentEquippedWeaponOrNull.CopyData(inventoryItemCardInDetailPanel.card.GetCopiedData());
                inventoryManager.weaponDisplay.card.CopyData(PlayerInformation.inventory.currentEquippedWeaponOrNull);
                break;
            default:
                Debug.Assert(false);
                break;
        }
        if(NetworkManager.instance != null)
        {
            // 현재 장비한 아이템을 서버로 전송
            yield return StartCoroutine(NetworkManager.instance.ISaveEquippedItemToServer(inventoryItemCardInDetailPanel.card.id, inventoryItemCardInDetailPanel.card.type));
        }
        
        Debug.Log("실행!");
        inventoryManager.UpdateUI(); // 인벤토리 UI들을 초기화 한다.
        detailItemCardPopup.SetActive(false);
    }

    /// <summary>
    /// 판매 버튼 클릭 시 호출
    /// </summary>
    public void OnClick_SaleBtn()
    {
        PopupManager.instance.OpenItemSaleConfirmPopup(true); // 아이템 세일 확인 팝업 열기
    }

    /// <summary>
    /// 장착 해제 버튼을 클릭 시 호출
    /// </summary>
    public void OnClick_UnEquipBtn()
    {
        StartCoroutine(IOnClick_UnEquipBtn());
    }
    private IEnumerator IOnClick_UnEquipBtn()
    {
        // 서버 연동
        if(NetworkManager.instance != null)
        {
            yield return StartCoroutine(NetworkManager.instance.IDeleteEquippedItemToServer(inventoryItemCardInDetailPanel.card.type));
        } else
        {
            Debug.LogWarning("NetworkManager가 없습니다. 서버에 장착 해제 정보를 보낼 수 없습니다.");
        }

        // TODO (장현명) : 나중에 리팩토링 하기
        switch (inventoryItemCardInDetailPanel.card.type)
        {
            case EEquipmentItemType.armor:
                inventoryManager.armorDisplay.gameObject.SetActive(false);
                PlayerInformation.inventory.currentEquippedArmorOrNull.Reset();
                break;
            case EEquipmentItemType.helmet:
                inventoryManager.helmetDisplay.gameObject.SetActive(false);
                PlayerInformation.inventory.currentEquippedHelmetOrNull.Reset();
                break;
            case EEquipmentItemType.shoes:
                inventoryManager.shoesDisplay.gameObject.SetActive(false);
                PlayerInformation.inventory.currentEquippedShoesOrNull.Reset();
                break;
            case EEquipmentItemType.weapon:
                inventoryManager.weaponDisplay.gameObject.SetActive(false);
                PlayerInformation.inventory.currentEquippedWeaponOrNull.Reset();
                break;
            default:
                Debug.Assert(false, "아이템 타입 범위 초과");
                break;
        }
        inventoryManager.UpdateUI(); // 인벤토리 UI들을 초기화 한다.
        detailItemCardPopup.SetActive(false);
        PopupManager.instance.OpenAlarmPopup("장착이 해제되었습니다.");

    }
    /// <summary>
    /// 팝업창을 닫는 버튼 클릭 시 호출
    /// </summary>
    public void OnClick_CloseBtn()
    {
        detailItemCardPopup.SetActive(false);
    }

    /// <summary>
    /// 장착버튼이나 장착 해제 버튼을 바꾼다.
    /// </summary>
    private void ChangeEquipOrUnEquipButton()
    {
        if (NetworkManager.instance == null)
        {
            Debug.LogWarning("NetworkManager에서 장비 id를 가져 올 수 없어서 아이템 장착 해제버튼을 보여줄 수 없습니다. (NetworkManage가 있는지 확인하십시오)");
            return;
        }
        // TODO (장현명) : 배열로 처리하면 코드가 간단히 만들 수 있다. 나중에 리팩토링

        // 현재 아이템이 장착되어있는지 확인하여 장착버튼, 해제 버튼으로 바꾸기
        switch (inventoryItemCardInDetailPanel.card.type)
        {
            case EEquipmentItemType.armor:
                if (PlayerInformation.inventory.currentEquippedArmorOrNull != null)
                {
                    if (PlayerInformation.inventory.currentEquippedArmorOrNull.id == inventoryItemCardInDetailPanel.card.id)
                    {
                        unEquipButton.SetActive(true);
                        equipButton.SetActive(false);
                    }
                    else
                    {
                        unEquipButton.SetActive(false);
                        equipButton.SetActive(true);
                    }
                }

                break;
            case EEquipmentItemType.helmet:
                if (PlayerInformation.inventory.currentEquippedHelmetOrNull != null)
                {
                    if (PlayerInformation.inventory.currentEquippedHelmetOrNull.id == inventoryItemCardInDetailPanel.card.id)
                    {
                        unEquipButton.SetActive(true);
                        equipButton.SetActive(false);
                    }
                    else
                    {
                        unEquipButton.SetActive(false);
                        equipButton.SetActive(true);
                    }
                }

                break;
            case EEquipmentItemType.shoes:
                if (PlayerInformation.inventory.currentEquippedShoesOrNull != null)
                {
                    if (PlayerInformation.inventory.currentEquippedShoesOrNull.id == inventoryItemCardInDetailPanel.card.id)
                    {
                        unEquipButton.SetActive(true);
                        equipButton.SetActive(false);
                    }
                    else
                    {
                        unEquipButton.SetActive(false);
                        equipButton.SetActive(true);
                    }
                }

                break;
            case EEquipmentItemType.weapon:
                if (PlayerInformation.inventory.currentEquippedWeaponOrNull != null)
                {
                    if (PlayerInformation.inventory.currentEquippedWeaponOrNull.id == inventoryItemCardInDetailPanel.card.id)
                    {
                        unEquipButton.SetActive(true);
                        equipButton.SetActive(false);
                    }
                    else
                    {
                        unEquipButton.SetActive(false);
                        equipButton.SetActive(true);
                    }
                }

                break;
            default:
                Debug.Assert(false, "아이템 타입 범위 초과");
                break;
        }
    }
}
