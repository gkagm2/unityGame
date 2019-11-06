using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 유저의 인벤토리
/// </summary>
public class UserInventory
{
    public EquipmentItem currentEquippedWeaponOrNull;       // 착용중인 무기 아이템
    public EquipmentItem currentEquippedHelmetOrNull;       // 착용중인 헬멧 아이템
    public EquipmentItem currentEquippedShoesOrNull;        // 착용중인 신발 아이템
    public EquipmentItem currentEquippedArmorOrNull;        // 착용중인 아머 아이템

    public List<ConsumableItem> consumableItemDataList;      // 현재 가지고 있는 소비 아이템들의 데이터
    public List<EquipmentItem> equipmentItemDataList;        // 현재 가지고 있는 장비 아이템들의 데이터

    public int maxSlotSpace;            // 인벤토리의 최대 슬롯 공간 (무기 = maxSlotSpace, 방어구 = maxSlotSpace)
    public int defenceItemCount
    {
        get
        {
            int itemCount = 0;
            for (int i = 0; i < equipmentItemDataList.Count; ++i)
            {
                if(equipmentItemDataList[i].type == EEquipmentItemType.armor ||
                   equipmentItemDataList[i].type == EEquipmentItemType.helmet ||
                   equipmentItemDataList[i].type == EEquipmentItemType.shoes)
                {
                    ++itemCount;
                }
            }
            return itemCount;
        }
    }      // 방어구 아이템 개수
    public int weaponItemCount
    {
        get
        {
            int itemCount = 0;
            for(int i=0;i < equipmentItemDataList.Count; ++i)
            {
                if(equipmentItemDataList[i].type == EEquipmentItemType.weapon)
                {
                    ++itemCount;
                }
            }
            return itemCount;
        }
    }       // 무기 아이템 개수

    public UserInventory()
    {
        Debug.Log("초기화!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        currentEquippedWeaponOrNull = new EquipmentItem();
        currentEquippedHelmetOrNull = new EquipmentItem();
        currentEquippedShoesOrNull = new EquipmentItem();
        currentEquippedArmorOrNull = new EquipmentItem();
        currentEquippedArmorOrNull.Reset();
        currentEquippedHelmetOrNull.Reset();
        currentEquippedShoesOrNull.Reset();
        currentEquippedWeaponOrNull.Reset();

        consumableItemDataList = new List<ConsumableItem>();
        equipmentItemDataList = new List<EquipmentItem>();
        maxSlotSpace = 20; // 최대 슬롯 공간의 개수 20개
    }

    /// <summary>
    /// 유저의 정보를 초기화한다.
    /// </summary>
    public void InitUserInventory()
    {
        consumableItemDataList.Clear();
        equipmentItemDataList.Clear();
        currentEquippedWeaponOrNull.Reset();
        currentEquippedHelmetOrNull.Reset();
        currentEquippedShoesOrNull.Reset();
        currentEquippedArmorOrNull.Reset();
    }

    /// <summary>
    /// HP 포션의 개수를 구한다.
    /// </summary>
    /// <returns>가지고 있는 포션의 개수</returns>
    public int GetHpPotionCount()
    {
        int potionCount = 0;
        //Debug.Log("consumableItemData count : " + consumableItemDataList.Count);
        for (int i = 0; i < consumableItemDataList.Count; ++i)
        {
            Debug.Log("i : " + i);
            // 포션이면
            if (consumableItemDataList[i].type == EConsumableItemType.hpPotion)
            {
                ++potionCount;
            }
        }
        return potionCount;
    }

    /// <summary>
    /// HP포션 아이템을 사용한다.
    /// </summary>
    /// <returns>해당 아이템의 id를 리턴한다.</returns>
    public int UseHpPotionItem()
    {
        int consumableItemId = -1;
        int potionCount = GetHpPotionCount();
        if (potionCount <= 0) //포션 개수가 없으면
        {
            Debug.Log(" 포션 없음");
            return consumableItemId; // -1 리턴
        }

        for (int i = 0; i < consumableItemDataList.Count; ++i)
        {
            if (consumableItemDataList[i].type == EConsumableItemType.hpPotion)
            {
                consumableItemId = consumableItemDataList[i].id;
                consumableItemDataList.RemoveAt(i); //삭제
                break;
            }
        }
        return consumableItemId;
    }


    /// <summary>
    /// 리스트 안에 장비 아이템이 있는지 찾고 찾으면 장비 아이템의 인덱스값, 못찾으면 -1을 리턴한다.
    /// </summary>
    /// <param name="searchId">찾을 장비 아이템의 id값</param>
    /// <returns>찾지 못하면 null값을 리턴, 찾으면 EquipmentItem 값 리턴</returns>
    public int GetEquipmentItemId(int searchId)
    {
        // Search Item
        for (int idx = 0; idx < PlayerInformation.inventory.equipmentItemDataList.Count; ++idx)
        {
            if (PlayerInformation.inventory.equipmentItemDataList[idx].id == searchId)
            {
                Debug.Log("장비 아이템을 찾았습니다." + PlayerInformation.inventory.equipmentItemDataList[idx].name +","+ PlayerInformation.inventory.equipmentItemDataList[idx].id);
                return idx; // 찾으면
            }
        }
        return -1;
    }
}
