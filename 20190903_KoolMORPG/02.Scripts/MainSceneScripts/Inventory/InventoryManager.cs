using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Inventory 화면을 관리하는 클래스
/// </summary>
public class InventoryManager : MonoBehaviour
{
    [Header("Inventory panel")]
    public Text hpText;                         // 체력 텍스트
    public Text totalAttackPowerText;            // 총공격력 텍스트
    public Text totalDefencePowerText;          // 총방어력 텍스트
    [Header(".")]
    public EquippedItemDisplay helmetDisplay;
    public EquippedItemDisplay armorDisplay;
    public EquippedItemDisplay shoesDisplay;
    public EquippedItemDisplay weaponDisplay;
    public Image hpPotionImage;
    public Text hpPotionCountText;

    public Image[] tapButtonsImage;             // 0 : weapon, 1 : defence (ETapMenu 의 번호를 따라감)

    public Text currentSlotSpaceText;
    
    public GameObject scrollViewContent;        // 아이템 카드를 생성 할 스크롤 뷰 Content
    public GameObject inventoryItemCard;        // 인벤토리 아이템 카드

    public List<GameObject> itemCardsList;      // 아이템 카드를 담은 리스트
    public List<int> choicedItemCards;          // 선택 된 아이템 카드의 id를 담은 리스트.

    

    public enum EItemChoiceMode
    {
        normal,     // 평상시 모드
        saleByBulk, // 일괄 판매 모드
        selectSale  // 선택 판매 모드
    }
    public enum ETapMenu
    {
        weapon,
        defence
    }

    public EItemChoiceMode eItemChoiceMode;
    public ETapMenu eCurrentTapMenu;

    // Test용 코드 
    public int equipmentItemIndex = 0;
    // Test용 코드 끝

    private void Awake()
    {
        Debug.Log("itemCardList 생성");
        itemCardsList = new List<GameObject>();
        choicedItemCards = new List<int>();
    }

    /// <summary>
    /// 처음 시작 시 인벤토리 UI들을 초기화 한다.
    /// </summary>
    public void InitUI()
    {
        eCurrentTapMenu = ETapMenu.weapon; // 탭 창을 무기 창으로 초기화한다.
        UpdateUI();
    }

    /// <summary>
    /// 인벤토리 UI를 업데이트한다.
    /// </summary>
    public void UpdateUI()
    {
        ChangeTap(eCurrentTapMenu);             // 탭을 업데이트 한다.
        
        hpPotionImage.sprite = Resources.Load<Sprite>(PathOfResources.consumableImages + "hpPotion");   // 포션의 이미지를 업데이트한다.
        hpPotionCountText.text = PlayerInformation.inventory.GetHpPotionCount().ToString();             // 포션의 개수를 보여준다.
        currentSlotSpaceText.text = PlayerInformation.inventory.equipmentItemDataList.Count.ToString() + "/" + PlayerInformation.inventory.maxSlotSpace.ToString(); // 슬롯의 공간 상태를 보여준다.

        hpText.text = PlayerInformation.userData.hp.ToString();                       // 체력 텍스트
        totalAttackPowerText.text = PlayerInformation.userData.TotalAtk.ToString();   // 총공격력 텍스트
        totalDefencePowerText.text = PlayerInformation.userData.TotalDef.ToString();  // 총방어력 텍스트
        InsertEquipmentItemDataToItemCardsList();   // 아이템카드 생성하여 리스트에 넣기
        ShowItemCardInContent(eCurrentTapMenu);     // 아이템 카드들을 content에 보여준다.
        UpdateCurrentEquippedItems();               // 착용중인 아이템들의 UI 업데이트

    }

    /// <summary>
    /// 착용중인 아이템들의 UI를 업데이트 한다.
    /// </summary>
    public void UpdateCurrentEquippedItems()
    {
        if(PlayerInformation.inventory.currentEquippedHelmetOrNull.type != EEquipmentItemType.none)
        {
            Debug.Log("착용된 헬멧을 보여준다.");
            helmetDisplay.gameObject.SetActive(true);
            helmetDisplay.card.CopyData(PlayerInformation.inventory.currentEquippedHelmetOrNull);
            helmetDisplay.UpdateUI();
        }
        else
        {
            helmetDisplay.gameObject.SetActive(false);
        }

        if(PlayerInformation.inventory.currentEquippedArmorOrNull.type != EEquipmentItemType.none)
        {
            Debug.Log("착용된 갑옷을 보여준다.");
            armorDisplay.gameObject.SetActive(true);
            armorDisplay.card.CopyData(PlayerInformation.inventory.currentEquippedArmorOrNull);
            armorDisplay.UpdateUI();
        }
        else
        {
            armorDisplay.gameObject.SetActive(false);
        }
        
        if(PlayerInformation.inventory.currentEquippedShoesOrNull.type != EEquipmentItemType.none)
        {
            Debug.Log("착용된 슈즈를 보여준다.");
            shoesDisplay.gameObject.SetActive(true);
            shoesDisplay.card.CopyData(PlayerInformation.inventory.currentEquippedShoesOrNull);
            shoesDisplay.UpdateUI();
        }
        else
        {
            shoesDisplay.gameObject.SetActive(false);
        }
        
        if(PlayerInformation.inventory.currentEquippedWeaponOrNull.type != EEquipmentItemType.none)
        {
            Debug.Log("착용된 무기를 보여준다.");
            weaponDisplay.gameObject.SetActive(true);
            weaponDisplay.card.CopyData(PlayerInformation.inventory.currentEquippedWeaponOrNull);
            weaponDisplay.UpdateUI();
        }
        else
        {
            weaponDisplay.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 무기 탭 버튼을 눌렀을 때 호출하는 함수
    /// </summary>
    public void OnClick_WeaponTapBtn()
    {
        eCurrentTapMenu = ETapMenu.weapon;
        ChangeTap(eCurrentTapMenu);
        ShowItemCardInContent(eCurrentTapMenu); // 아이템 카드들을 content에 보여준다.
        Debug.Log("무기 탭 버튼을 눌렀습니다.");
    }

    /// <summary>
    /// 방어구 탭 버튼을 눌렀을 때 호출하는 함수
    /// </summary>
    public void OnClick_DefenceTapBtn()
    {
        eCurrentTapMenu = ETapMenu.defence;
        ChangeTap(eCurrentTapMenu);
        ShowItemCardInContent(eCurrentTapMenu); // 아이템 카드들을 content에 보여준다.
        Debug.Log("방어구 탭 버튼을 눌렀습니다.");
    }

    /// <summary>
    /// 일괄 판매 버튼 클릭 시 호출하는 함수
    /// </summary>
    public void OnClick_SaleByBulkBtn()
    {
        if(eItemChoiceMode == EItemChoiceMode.normal) // normal 모드이면
        {
            eItemChoiceMode = EItemChoiceMode.saleByBulk;   // 일괄 판매 모드로 전환.
            Debug.Log("일괄 판매 모드로 전환함.");
        }
        else // normal모드가 아니면 
        {
            eItemChoiceMode = EItemChoiceMode.normal; // normal 모드로 전환.
            Debug.Log("normal모드로 전환함.");
        }
    }

    /// <summary>
    /// 선택 판매 버튼 클릭 시 호출하는 함수
    /// </summary>
    public void OnClick_SelectSaleBtn()
    {
        if(eItemChoiceMode == EItemChoiceMode.normal) // normal 모드이면
        {
            eItemChoiceMode = EItemChoiceMode.selectSale; // 선택 판매 모드로 전환
            Debug.Log("선택 판매 모드로 전환함.");
        }
        else // normal모드가 아니면
        {
            eItemChoiceMode = EItemChoiceMode.normal; // nmormal 모드로 전환
            Debug.Log("normal모드로 전환함.");
        }
    }

    /// <summary>
    /// 장비 아이템을 판매한다.
    /// </summary>
    /// <param name="">판매 할 아이템</param>
    public void SaleEquipmentItem(EquipmentItem item)
    {
        StartCoroutine(ISaleEquipmentItem(item));
    }
    private IEnumerator ISaleEquipmentItem(EquipmentItem item)
    {
        PlayerInformation.userData.gold += item.gold;
        RemoveEquipmentItemInPlayerInformation(item);
        if(NetworkManager.instance != null)
        {
            yield return StartCoroutine(NetworkManager.instance.IDeleteEquipmentItemToServer(item.id));
            yield return StartCoroutine(NetworkManager.instance.ISaveCharacterDataToServer());
        }
        else
        {
            Debug.LogWarning("NetworkManager가 없어서 판매된 후 상태를 저장 할 수 없습니다.");
        }
        TopPopupManager.instance.TopPopupsUpdate();
        PopupManager.instance.OpenAlarmPopup("판매 완료");
        InitItemCardSetting();
    }

    /// <summary>
    /// PlayerInformation의 리스트에 있는 장비 아이템을 없앤다.
    /// </summary>
    /// <param name="">판매 할 아이템</param>
    private void RemoveEquipmentItemInPlayerInformation(EquipmentItem item)
    {
        bool isRemove = false;
        // 장비 아이템의 id값을 비교하여 삭제할 item의 id값이 맞으면 리스트에서 삭제한다.
        for (int i = 0; i < PlayerInformation.inventory.equipmentItemDataList.Count; ++i)
        {
            if (PlayerInformation.inventory.equipmentItemDataList[i].id == item.id)
            {
                PlayerInformation.inventory.equipmentItemDataList.RemoveAt(i); // 삭제한다.
                isRemove = true;
                break;
            }
        }

        if (isRemove)
        {
            Debug.Log("리스트에서 아이템 삭제 완료");
        }
        else
        {
            Debug.Log("리스트에서 아이템 삭제 안됨");
        }
    }

    /// <summary>
    /// 아이템 카드를 컨텐츠에 보여준다.
    /// </summary>
    private void ShowItemCardInContent(ETapMenu tapMenu)
    {
        switch (tapMenu)
        {
            case ETapMenu.weapon: 
                // 무기 아이템 카드만 보이게 한다.
                for(int i=0; i < itemCardsList.Count; ++i)
                {
                    if (itemCardsList[i].GetComponent<InventoryItemDisplay>().card.type == EEquipmentItemType.weapon)
                    {
                        itemCardsList[i].SetActive(true);
                    }
                    else
                    {
                        itemCardsList[i].SetActive(false);
                    }
                }
                break;
            case ETapMenu.defence:
                // 무기 아이템을 제외한 아이템 카드만 보이게 한다.
                for (int i = 0; i < itemCardsList.Count; ++i)
                {
                    if (itemCardsList[i].GetComponent<InventoryItemDisplay>().card.type == EEquipmentItemType.weapon)
                    {
                        itemCardsList[i].SetActive(false);
                    }
                    else
                    {
                        itemCardsList[i].SetActive(true);
                    }
                }
                break;
            default:
                Debug.Assert(false);
                break;
        }
    }

    /// <summary>
    /// 장비 아이템 카드들을 초기 세팅 해준다.
    /// </summary>
    private void InitItemCardSetting()
    {
        InsertEquipmentItemDataToItemCardsList(); // 아이템 카드에 장비 아이템 데이터를 삽입한다.
        ShowItemCardInContent(eCurrentTapMenu);
    }
    
    /// <summary>
    /// 아이템 카드 리스트에 장비 아이템 데이터들을 삽입한다.
    /// </summary>
    private void InsertEquipmentItemDataToItemCardsList()
    {
        while (itemCardsList.Count > 0)
        {
            GameObject itemCard = itemCardsList[0];
            itemCardsList.RemoveAt(0);
            Destroy(itemCard);
        }

        // 플레이어 아이템 리스트의 정보를 가져와 아이템 카드 생성
        for (int i = 0; i < PlayerInformation.inventory.equipmentItemDataList.Count; i++)
        {
            GameObject itemCard = Instantiate(inventoryItemCard, transform.position, transform.rotation, scrollViewContent.transform) as GameObject;
            if (itemCard) // 아이템 카드를 생성했으면
            {
                InventoryItemDisplay item = itemCard.GetComponent<InventoryItemDisplay>();
                if (item) // 아이템이 존재한다면
                {
                    item.card.CopyData(PlayerInformation.inventory.equipmentItemDataList[i]);
                    item.UpdateUI();
                }
                else
                {
                    Debug.LogError("아이템이 존재하지 않는다!");
                }
                itemCardsList.Add(itemCard); // 리스트에 집어넣는다.
            }
            else
            {
                Debug.LogError("아이템 카드를 생성하지 못했습니다.");
            }
        }
    }

    /// <summary>
    /// 탭 버튼을 변경 할 때 호출되는 함수.
    /// 탭 버튼의 색상을 변경하고 
    /// </summary>
    /// <param name="tapMenu">바꿀 탭 메뉴 종류</param>
    private void ChangeTap(ETapMenu tapMenu)
    {
        ChangeTapColor(tapMenu); // 탭 버튼의 색상 변경

        switch (tapMenu)
        {
            case ETapMenu.weapon:
                ChangeTapColor(tapMenu); // 탭 버튼의 색상 변경
                break;

            case ETapMenu.defence:
                ChangeTapColor(tapMenu); // 탭 버튼의 색상 변경
                break;
            default:
                Debug.Assert(false);
                break;
        }
    }

    /// <summary>
    /// 선택한 탭의 버튼 색상을 초록색으로 변경하고 선택되지 않은 버튼 색상을 원래 색으로 변경한다.
    /// </summary>
    /// <param name="tapMenu">변경 할 탭 메뉴</param>
    private void ChangeTapColor(ETapMenu tapMenu)
    {
        for (int i = 0; i < tapButtonsImage.Length; i++)
        {
            if ((int)tapMenu == i)
            {
                tapButtonsImage[i].color = Color.green;
            }
            else
            {
                tapButtonsImage[i].color = Color.white;
            }
        }
    }
}
