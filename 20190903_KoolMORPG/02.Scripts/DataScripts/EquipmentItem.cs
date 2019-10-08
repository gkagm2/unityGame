using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장비류 아이템의 데이터 정보를 담고있는 클래스
/// </summary>
public class EquipmentItem
{
    public int id;                      // Item id;
    public string name;                 // Item name
    public ETierType tier;
    public EEquipmentItemType type;
    public float atk;                   // Attack power
    public float def;                   // Defence power
    public float moveSpeed;
    public float attackSpeed;
    public int gold;
    public ETendency tendency;
    public int reinforcementCount;
    public int imageNumber;             // 0에서 시작
    public Sprite Icon
    {
        get
        {
            string path = "";
            switch (type){
                case EEquipmentItemType.armor:
                    path = PathOfResources.armorImages + imageNumber;
                    break;
                case EEquipmentItemType.helmet:
                    path = PathOfResources.helmetImages + imageNumber;
                    break;
                case EEquipmentItemType.shoes:
                    path = PathOfResources.shoesImages + imageNumber;
                    break;
                case EEquipmentItemType.weapon: // 무기 타입일 경우
                    switch (PlayerInformation.userData.characterType) // 캐릭터에 따라 Resources 경로를 다르게 준다.
                    {
                        case EPlayerCharacterType.warrior:
                            path = PathOfResources.warriorWeaponImages + imageNumber;
                            break;
                        case EPlayerCharacterType.archer:
                            path = PathOfResources.archarWeaponImages + imageNumber;
                            break;
                        default:
                            Debug.LogWarning("캐릭터 타입이 설정되어있지 않음 이미지를 넣을 수 없음(CharacterChoiceScene에서 시작)");
                            break;
                    }
                    break;
                case EEquipmentItemType.none:
                    break;
                default:
                    Debug.Assert(false, "무기 type 범위 초과");
                    break;
            }
            return Resources.Load<Sprite>(path);
        }
    }

    /// <summary>
    /// 값을 복사한다.
    /// </summary>
    /// <param name="data">복사할 장비 아이템</param>
    public void CopyData(EquipmentItem data)
    {
        id = data.id;
        name = data.name;
        gold = data.gold;
        reinforcementCount = data.reinforcementCount;
        atk = data.atk;
        def = data.def;
        moveSpeed = data.moveSpeed;
        attackSpeed = data.attackSpeed;
        tier = data.tier;
        tendency = data.tendency;
        type = data.type;
        imageNumber = data.imageNumber;
        //Debug.Log("Copied -> id : " + id + ", name : " + name + ", gold : " + gold + ", reinforcementCount : " + reinforcementCount + ", atk : " + atk + ", def : " + def + ", moveSpeed : " + moveSpeed + ", attackSpeed : " + attackSpeed + ", tier : " + tier + ", tendency : " + tendency + ", type : " + type + ", imageNumber : " + imageNumber + ", icon : " + icon);
    }

    /// <summary>
    /// 현재 클래스의 데이터 값들을 복사한 데이터를 반환한다.
    /// </summary>
    /// <returns>반환할 데이터</returns>
    public EquipmentItem GetCopiedData()
    {
        EquipmentItem data = new EquipmentItem();
        data.CopyData(this);
        return data;
    }

    /// <summary>
    /// 리셋한다.
    /// </summary>
    public void Reset()
    {
        id = -1;
        name = "";
        gold = 0;
        reinforcementCount = 0;
        atk = 0;
        def = 0; ;
        moveSpeed = 0; ;
        attackSpeed = 0; ;
        tier = 0;
        tendency = 0; ;
        type = EEquipmentItemType.none;
        imageNumber = -1;
    }
}
