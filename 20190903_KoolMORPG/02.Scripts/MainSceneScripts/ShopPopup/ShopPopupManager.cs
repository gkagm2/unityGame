using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 상점 패널을 관리하는 클래스
/// </summary>
public class ShopPopupManager : MonoBehaviour
{
    public ShopItem currentChoicedItem;             // 상점의 아이템 클릭 시 선택된 아이템의 정보를 담는다.

    public GameObject equipmentCategoryPanel;       // 장비 카테고리 패널
    public GameObject potionCategoryPanel;          // 포션 카테고리 패널
    public GameObject crystalAndGoldCategoryPanel;  // 크리스탈 & 골드 카테고리 패널

    public Image equipmentCategoryButtonImage;      // 장비 카테고리 버튼의 이미지
    public Image potionCategoryButtonImage;         // 포션 카테고리 버튼의 이미지
    public Image crystalAndGoldCategoryButtonImage; // 크리스탈 & 골드 카테고리 버튼의 이미지

    [Header("Purchase Confirmation")]
    public GameObject reconfirmPopup;               // 재확인 팝업
    public GameObject suggestionPopup;              // 크리스탈 구매 제안 팝업


    [Header("Random Items")]
    public EquipmentItemData[] randomShoesItems;
    public EquipmentItemData[] randomHelmetItems;
    public EquipmentItemData[] randomArmorItems;
    public EquipmentItemData[] randomSwordItems;
    public EquipmentItemData[] randomBowItems;
    public EquipmentItemData randomItemData;      // 선택된 랜덤 아이템

    public enum ECategoryType
    {
        equipmentCategory,
        potionCategory,
        crystalAndGoldCategory
    }
    void Start()
    {
        InitShopUI();
    }

    /// <summary>
    /// Shop의 UI를 초기화 해준다.
    /// </summary>
    public void InitShopUI()
    {
        SetCategory(ECategoryType.equipmentCategory);
    }

    /// <summary>
    /// 장비 카테고리 버튼 클릭 시 호출.
    /// </summary>
    /// <param name="isOpen">패널 On/Off 플래그</param>
    public void OnClick_EquiopmentCategoryPanelBtn(bool isOpen)
    {
        if (isOpen)
        {
            SetCategory(ECategoryType.equipmentCategory);
        }
        else
        {
            equipmentCategoryPanel.SetActive(false);
        }
    }

    /// <summary>
    /// 포션 카테고리 버튼 클릭 시 호출
    /// </summary>
    /// <param name="isOpen">패널 On/Off 플래그</param>
    public void OnClick_PotionCategoryPanelBtn(bool isOpen)
    {
        if (isOpen)
        {
            SetCategory(ECategoryType.potionCategory);
        }
        else
        {
            potionCategoryPanel.SetActive(false);
        }
    }

    /// <summary>
    /// 크리스탈 & 골드 카테고리 버튼 클릭 시 호출
    /// </summary>
    /// <param name="isOpen">패널 On/Off 플래그</param>
    public void OnClick_CrystalAndGoldCategoryPanelBtn(bool isOpen)
    {
        if (isOpen)
        {
            SetCategory(ECategoryType.crystalAndGoldCategory);
        }
        else
        {
            crystalAndGoldCategoryPanel.SetActive(false);
        }
    }

    /// <summary>
    /// enum으로 바꾸기
    /// </summary>
    /// <param name="category"></param>
    public void SetCategory(ECategoryType eCategoryType)
    {
        switch (eCategoryType)
        {
            case ECategoryType.equipmentCategory:
                equipmentCategoryPanel.SetActive(true);
                potionCategoryPanel.SetActive(false);
                crystalAndGoldCategoryPanel.SetActive(false);
                equipmentCategoryButtonImage.color = Color.green;
                potionCategoryButtonImage.color = Color.white;
                crystalAndGoldCategoryButtonImage.color = Color.white;
                break;
            case ECategoryType.crystalAndGoldCategory:
                crystalAndGoldCategoryPanel.SetActive(true);
                equipmentCategoryPanel.SetActive(false);
                potionCategoryPanel.SetActive(false);
                equipmentCategoryButtonImage.color = Color.white;
                potionCategoryButtonImage.color = Color.white;
                crystalAndGoldCategoryButtonImage.color = Color.green;
                break;
            case ECategoryType.potionCategory:
                potionCategoryPanel.SetActive(true);
                equipmentCategoryPanel.SetActive(false);
                crystalAndGoldCategoryPanel.SetActive(false);
                equipmentCategoryButtonImage.color = Color.white;
                potionCategoryButtonImage.color = Color.green;
                crystalAndGoldCategoryButtonImage.color = Color.white;
                break;
            default:
                Debug.Assert(false, "카테고리 타입이 다름");
                break;
        }
    }

    /// <summary>
    /// 크리스탈 구매 화면으로 가는것을 제한하는 팝업창 On/Off
    /// </summary>
    /// <param name="isOpen"></param>
    public void OpenSuggestionPopup(bool isOpen)
    {
        if (isOpen)
        {
            suggestionPopup.SetActive(true);
        }
        else
        {
            suggestionPopup.SetActive(false);
        }
    }

    /// <summary>
    /// 재확인 팝업 창 On/Off
    /// </summary>
    /// <param name="isOpen"></param>
    public void OpenReconfirmPopup(bool isOpen)
    {
        if (isOpen)
        {
            reconfirmPopup.SetActive(true);
            reconfirmPopup.GetComponent<ReconfirmPopup>().UpdateUI();
        }
        else
        {
            reconfirmPopup.SetActive(false);
        }
    }

    /// <summary>
    /// 아이템 구매를 한다.
    /// </summary>
    public void PurchaseItem()
    {
        
        switch (currentChoicedItem.shopItemType)
        {
            case EShopItemType.weapon:
                if (PlayerInformation.inventory.weaponItemCount < PlayerInformation.inventory.maxSlotSpace)
                {
                    PlayerInformation.userData.crystal -= currentChoicedItem.cost;
                    StartCoroutine(ISaveCharacterData());
                    PurchaseEquipmentItem();
                }
                else
                {
                    PopupManager.instance.OpenAlarmPopup("아이템 공간이 꽉찼습니다.");
                    return;
                }
                break;
            case EShopItemType.defence:
                if (PlayerInformation.inventory.defenceItemCount < PlayerInformation.inventory.maxSlotSpace)
                {
                    PlayerInformation.userData.crystal -= currentChoicedItem.cost;
                    StartCoroutine(ISaveCharacterData());
                    PurchaseEquipmentItem();
                }
                else
                {
                    PopupManager.instance.OpenAlarmPopup("아이템 공간이 꽉찼습니다.");
                    return;
                }
                break;
            case EShopItemType.weaponPackage:
                if (PlayerInformation.inventory.weaponItemCount + currentChoicedItem.purchaseCount < PlayerInformation.inventory.maxSlotSpace)
                {
                    PlayerInformation.userData.crystal -= currentChoicedItem.cost;
                    StartCoroutine(ISaveCharacterData());
                    PurchaseEquipmentItemPack();
                }
                else
                {
                    PopupManager.instance.OpenAlarmPopup("아이템 공간이 꽉찼습니다.");
                    return;
                }
                break;
            case EShopItemType.defencePackage:
                if (PlayerInformation.inventory.defenceItemCount + currentChoicedItem.purchaseCount < PlayerInformation.inventory.maxSlotSpace)
                {
                    PlayerInformation.userData.crystal -= currentChoicedItem.cost;
                    StartCoroutine(ISaveCharacterData());
                    PurchaseEquipmentItemPack();
                }
                else
                {
                    PopupManager.instance.OpenAlarmPopup("아이템 공간이 꽉찼습니다.");
                    return;
                }
                break;
            case EShopItemType.hpPotion:
                PurchaseHpPotion();
                break;
            case EShopItemType.stamina:
                PurchaseStamina();
                break;
            case EShopItemType.gold:
                PurchaseGold();
                break;
            default:
                Debug.Assert(false, "shop item type이 잘못 됨");
                break;
        }
        PopupManager.instance.OpenStandByPopup(true);
    }
    private IEnumerator ISaveCharacterData()
    {
        if (NetworkManager.instance != null)
        {
            // 캐릭터 데이터를 서버에 저장
            yield return StartCoroutine(NetworkManager.instance.ISaveCharacterDataToServer());
        }
        else
        {
            Debug.LogWarning("NetworkManager가 없음 서버에 아이템을 저장 할 수 없다.");
        }
    }

    /// <summary>
    /// 크리스탈을 구입한다. (GPPS)
    /// </summary>
    public void PurchaseCrystal()
    {
        // TODO : GPGS를 넣어야 (구현 X)
        PopupManager.instance.OpenAlarmPopup(currentChoicedItem.title + "미완성");
    }

    /// <summary>
    /// 장비 아이템 낱개로 구입한다.
    /// </summary>
    private void PurchaseEquipmentItem()
    {
        StartCoroutine(IPurchaseEquipmentItem());
    }
    private IEnumerator IPurchaseEquipmentItem()
    {
        EquipmentItem item;
        string itemName = "";
        int imageNumber = 0;
        int reinforcementCount = 1;
        ETierType tierType = ETierType.unique;
        EEquipmentItemType itemType = EEquipmentItemType.weapon; // default 값은 weapon

        // 아이템 생성
        for (int createCount = 0; createCount < currentChoicedItem.purchaseCount; ++createCount)
        {
            itemType = GetRandomEquipmentItem(out itemName, out imageNumber, itemType);
            Debug.Log("imageNumber : " + imageNumber + ", itemName : " + itemName + ", itemType : " + itemType.ToString());
            item = EventManager.MakeEquipmentItem(itemName, tierType, itemType, reinforcementCount, imageNumber);
            if (NetworkManager.instance != null)
            {
                yield return StartCoroutine(NetworkManager.instance.ISaveEquipmentItemsToServer(item));
            }
            else
            {
                Debug.LogWarning("NetworkManager가 없음 서버에 아이템을 저장 할 수 없다.");
            }
            PlayerInformation.inventory.equipmentItemDataList.Add(item);
        }
        UpdatePopup();
    }



    /// <summary>
    /// 장비 아이템팩을 구입한다.
    /// </summary>
    private void PurchaseEquipmentItemPack()
    {
        StartCoroutine(IPurchaseEquipmentItemPack());
    }
    private IEnumerator IPurchaseEquipmentItemPack()
    {
        EquipmentItem item;
        int imageNumber = 0;
        int reinforcementCount = 1;
        string itemName = "";
        ETierType tierType;
        EEquipmentItemType itemType = EEquipmentItemType.weapon; // default 값은 weapon

        // 아이템 생성
        for (int createCount = 0; createCount < currentChoicedItem.purchaseCount; ++createCount)
        {

            switch (currentChoicedItem.shopItemType)
            {
                case EShopItemType.weaponPackage:
                    itemType = EEquipmentItemType.weapon;
                    break;
                case EShopItemType.defencePackage:
                    itemType = (EEquipmentItemType)(UnityEngine.Random.Range(0, 3)); // 0: helmet, 1: armor, 2: shoes
                    break;
            } // 아이템 타입을 정해준다.

            itemType = GetRandomEquipmentItem(out itemName, out imageNumber, itemType);

            if (createCount == 0) // 전설 아이템은 하나만 생성되게 한다.
            {
                tierType = ETierType.legend; // 레전드 아이템으로 설정 변경
            }
            else
            {
                tierType = ETierType.unique; // 희귀 아이템으로 설정 변경
            }

            item = EventManager.MakeEquipmentItem(itemName, tierType, itemType, reinforcementCount, imageNumber);

            if (NetworkManager.instance != null)
            {
                yield return StartCoroutine(NetworkManager.instance.ISaveEquipmentItemsToServer(item));
            }
            else
            {
                Debug.LogWarning("NetworkManager가 없음 서버에 아이템을 저장 할 수 없다.");
            }
            PlayerInformation.inventory.equipmentItemDataList.Add(item);
        }
        UpdatePopup();
    }

    /// <summary>
    /// 현재 선택된 아이템의 타입에 따라 미리 데이터의 값이 저장되어있는 아이템으로 설정해준다.
    /// </summary>
    /// <param name="itemName">아이템 이름</param>
    /// <param name="imageNumber">이미지 번호</param>
    /// <param name="itemType">아이템 타입</param>
    /// <returns>설정 한 아이템 타입</returns>
    private EEquipmentItemType GetRandomEquipmentItem(out string itemName, out int imageNumber, EEquipmentItemType itemType)
    {
        switch (currentChoicedItem.shopItemType)
        {
            case EShopItemType.weapon:
            case EShopItemType.weaponPackage:
                itemType = EEquipmentItemType.weapon;
                switch (PlayerInformation.userData.characterType)
                {
                    case EPlayerCharacterType.archer:
                        randomItemData.CopyData(randomBowItems[Random.Range(0, randomBowItems.Length)]);
                        break;
                    case EPlayerCharacterType.warrior:
                        randomItemData.CopyData(randomSwordItems[Random.Range(0, randomSwordItems.Length)]);
                        break;
                    default:
                        Debug.Assert(false, "캐릭터 타입이 설정되어있지 않습니다.");
                        break;
                }
                break;
            case EShopItemType.defence:
            case EShopItemType.defencePackage:
                itemType = (EEquipmentItemType)(UnityEngine.Random.Range(0, 3)); // 0: helmet, 1: armor, 2: shoes
                switch (itemType)
                {
                    case EEquipmentItemType.armor:
                        randomItemData.CopyData(randomArmorItems[Random.Range(0, randomArmorItems.Length)]);
                        break;
                    case EEquipmentItemType.helmet:
                        randomItemData.CopyData(randomHelmetItems[Random.Range(0, randomHelmetItems.Length)]);
                        break;
                    case EEquipmentItemType.shoes:
                        randomItemData.CopyData(randomShoesItems[Random.Range(0, randomShoesItems.Length)]);
                        break;
                    default:
                        Debug.Assert(false);
                        break;
                }
                break;
        }

        imageNumber = randomItemData.imageNumber;
        itemName = randomItemData.name;
        return itemType;
    }

    /// <summary>
    /// HP 포션을 구입한다.
    /// </summary>
    private void PurchaseHpPotion()
    {
        StartCoroutine(IPurchaseHpPotion());
    }
    private IEnumerator IPurchaseHpPotion()
    {
        string itemName = "체력포션";
        int gold = 700;
        int imageNumber = 0;

        for (int i = 0; i < currentChoicedItem.purchaseCount; ++i)
        {
            ConsumableItem item = EventManager.MakeConsumableItem(itemName, gold, EConsumableItemType.hpPotion, imageNumber);
            if (NetworkManager.instance != null)
            {
                yield return NetworkManager.instance.IAddConsumableItemToServer(item);
            }
            else
            {
                Debug.LogWarning("NetworkManager가 없음 서버에 아이템을 저장 할 수 없다.");
            }
            PlayerInformation.inventory.consumableItemDataList.Add(item);
        }
        UpdatePopup();
    }

    /// <summary>
    /// 스테미나 구입한다.
    /// </summary>
    private void PurchaseStamina()
    {
        StartCoroutine(IPurchaseStamina());
    }
    private IEnumerator IPurchaseStamina()
    {
        PlayerInformation.userData.Stamina += currentChoicedItem.purchaseCount;
        if (NetworkManager.instance != null)
        {
            yield return StartCoroutine(NetworkManager.instance.ISaveCharacterDataToServer());
        }
        else
        {
            Debug.LogWarning("NetworkManager가 없음 서버에 캐릭터 stamina를 저장 할 수 없다.");
        }
        UpdatePopup();
    }

    /// <summary>
    /// 골드를 구입한다.
    /// </summary>
    private void PurchaseGold()
    {
        StartCoroutine(IPurchaseGold());
    }
    private IEnumerator IPurchaseGold()
    {
        PlayerInformation.userData.gold += currentChoicedItem.purchaseCount;
        if (NetworkManager.instance != null)
        {
            yield return StartCoroutine(NetworkManager.instance.ISaveCharacterDataToServer());
        }
        else
        {
            Debug.LogWarning("NetworkManager가 없음 서버에 캐릭터 gold를 저장 할 수 없다.");
        }
        UpdatePopup();
    }

    /// <summary>
    /// 팝업을 업데이트 한다.
    /// </summary>
    private void UpdatePopup()
    {
        PopupManager.instance.OpenStandByPopup(false);
        PopupManager.instance.OpenAlarmPopup(currentChoicedItem.title + "을/를 구매 완료 하였습니다.");
        TopPopupManager.instance.TopPopupsUpdate();
    }
}
