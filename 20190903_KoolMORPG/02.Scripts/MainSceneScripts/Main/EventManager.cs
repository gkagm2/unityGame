using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    #region Singleton
    public static EventManager instance;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }
    #endregion

    /// <summary>
    /// 랜덤으로 보상을 받는다.
    /// </summary>
    /// <param name="eRewardType">보상 받을 타입</param>
    public void ReceiveRandomReward()
    {
        
    }
    // TODO (장현명) : 보상 시스템 만들기  
    public void ReceiveReward(ERewardType rewardType)
    {

    }

    public void Receiveitem(EEquipmentItemType itemType)
    {

    }
    
    /// <summary>
    /// 장비 아이템을 생성한다.
    /// </summary>
    /// <param name="itemName">아이템의 이름</param>
    /// <param name="tier">아이템의 등급</param>
    /// <param name="itemType">아이템의 타입</param>
    /// <param name="reinforcementCount">아이템 강화 횟수(범위는 0~9)</param>
    /// <param name="imageNumber">아이템 이미지 번호</param>
    /// <returns>공식에 의해 설정된 아이템</returns>
    public static EquipmentItem MakeEquipmentItem(string itemName, ETierType tier, EEquipmentItemType itemType, int reinforcementCount, int imageNumber = 0)
    {
        reinforcementCount = Mathf.Clamp(reinforcementCount, 0, 9); // 0에서 9까지 강화 횟수 숫자 제한을 둠.

        EquipmentItem item = new EquipmentItem();

        float defaultAttackPower = 30.0f;  // 기본 공격력
        float defaultDefencePower = 10.0f; // 기본 방어력

        /// TODO (장현명) : 무기 강화값을 이용하여 뭔가 해야 함.
        // 무기 강화 값
        float weaponItemReinforcementValue = (int)tier * defaultAttackPower + UnityEngine.Random.Range(0, defaultAttackPower + 1) * reinforcementCount * 0.1f;
        // 방어구 강화 값
        float defenceItemReinforcementValue = (int)tier * defaultDefencePower + UnityEngine.Random.Range(5, defaultDefencePower + 1) * reinforcementCount * 0.1f;

        // 값 설정
        item.name = itemName;
        item.tier = tier;
        item.reinforcementCount = reinforcementCount;
        item.gold = (int)tier * 1000;
        item.type = itemType;
        item.imageNumber = imageNumber;

        switch (itemType) // Item Type에 따라 수치 값을 다르게 넣어준다.
        {
            case EEquipmentItemType.helmet:
                item.def = (int)item.tier * defaultDefencePower + UnityEngine.Random.Range(5, 11) + reinforcementCount; // 방어력
                item.atk = defaultAttackPower;
                item.attackSpeed = 0;
                item.moveSpeed = 0;
                item.tendency = ETendency.defensive; // 방어적인 성향
                break;

            case EEquipmentItemType.armor:
                item.def = (int)item.tier * defaultDefencePower + UnityEngine.Random.Range(5, 11) + reinforcementCount; // 방어력
                item.atk = defaultAttackPower;
                item.attackSpeed = 0;
                item.moveSpeed = 0;
                item.tendency = ETendency.defensive; // 방어적인 성향
                break;

            case EEquipmentItemType.shoes:
                item.def = (int)item.tier * defaultDefencePower + UnityEngine.Random.Range(5, 11) + reinforcementCount; // 방어력
                item.atk = defaultAttackPower;
                item.attackSpeed = 0;
                item.moveSpeed = 0; // FIXED (장현명) :  움직임 속도를 향상 할 수 있게 해야 함.
                item.tendency = ETendency.speedy; // 빠른 성향
                break;

            case EEquipmentItemType.weapon:
                item.def = 10;
                item.atk = (int)item.tier * defaultAttackPower + UnityEngine.Random.Range(5, 11) + reinforcementCount; // 공격력
                item.attackSpeed = 0;
                item.moveSpeed = 0;
                item.tendency = ETendency.aggressive; // 공격적인 성향
                break;

            default:
                Debug.Assert(false, "아이템 타입의 범위를 넘어갔다.");
                break;
        }
        return item;
    }

    /// <summary>
    /// 소비용 아이템을 생성한다.
    /// </summary>
    /// <param name="name">이름</param>
    /// <param name="gold">골드 가격</param>
    /// <param name="type">아이템 종류</param>
    /// <param name="imageNumber">아이템 이미지 번호</param>
    /// <returns>생성한 아이템</returns>
    public static ConsumableItem MakeConsumableItem(string name, int gold, EConsumableItemType type, int imageNumber = 0)
    {
        ConsumableItem item = new ConsumableItem();

        // 값을 설정 해준다.
        item.name = name;
        item.gold = gold;
        item.type = type;
        item.imageNumber = imageNumber;

        return item;
    }
}
