using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public string name;
    public string id;                        // Google player game service(GPGS) ID
    public float exp;                        // Experience point
    public float hp;                         // Health point
    public float def;                        // Defence power
    public float atk;                        // Attack power
    public float moveSpeed;
    public float attackSpeed;
    public int gold;
    public int crystal;
    public int level;
    public EPlayerCharacterType characterType;
    public int Stamina {
        get
        {
            return stamina;
        }
        set
        {
            stamina = value;
            Debug.Log("들어온 stamina : " + value);
            stamina = Mathf.Clamp(stamina, 0, MaxStamina);
            Debug.Log("설정된 stamina : " + stamina + ", 최대 스테미나 : " + MaxStamina);
        }
    }
    public int MaxStamina
    {
        get
        {
            int maxStamina = 0;
            if (level < 10)
            {
                maxStamina = (level + 1) * 5 + 5;
            }
            else
            {
                maxStamina = 60;
            }
            return maxStamina;
        }
    }                 // 레벨에 따른 최대 행동력
    public float MaxExp
    {
        get
        {
            return (level + 1) * 5 + 5;
        }
    }                   // 레벨에 따른 최대 경험치
    public float TotalDef
    {
        get
        {
            return PlayerInformation.inventory.currentEquippedWeaponOrNull.def + PlayerInformation.inventory.currentEquippedHelmetOrNull.def + PlayerInformation.inventory.currentEquippedArmorOrNull.def + PlayerInformation.inventory.currentEquippedShoesOrNull.def;
        }
    }                 // 합산된 방어력 장비 방어력 + 기본 방어력
    public float TotalAtk
    {
        get
        {
            return atk + PlayerInformation.inventory.currentEquippedWeaponOrNull.atk + PlayerInformation.inventory.currentEquippedArmorOrNull.atk + PlayerInformation.inventory.currentEquippedHelmetOrNull.atk + PlayerInformation.inventory.currentEquippedShoesOrNull.atk;
        }
    }                 // 합산된 공격력 장비 공격력 + 기본 공격력

    public Sprite characterFaceIcon;   // 캐릭터 얼굴 이미지

    private int stamina;

    /// <summary>
    /// 파라미테 값에 있는 데이터를 내부에 복사한다.
    /// </summary>
    /// <param name="data">복사할 유저 데이터</param>
    public void CopyData(UserData data)
    {
        name = data.name;
        id = data.id;
        exp = data.exp;
        hp = data.hp;
        def = data.def;
        atk = data.atk;
        moveSpeed = data.moveSpeed;
        attackSpeed = data.attackSpeed;
        gold = data.gold;
        crystal = data.crystal;
        level = data.level;
        characterType = data.characterType;
        Stamina = data.Stamina;
    }

    /// <summary>
    /// 재화를 설정한다.
    /// </summary>
    public void SetGoods(int gold = 0 , int crystal = 0)
    {
        this.gold = gold;
        this.crystal = crystal;
        //exp = 0;
        Stamina = MaxStamina;
    }

    /// <summary>
    /// 레벨과 캐릭터 타입에 따라 스텟을 설정한다.
    /// </summary>
    /// <param name="characterType">캐릭터 타입</param>
    /// <param name="level">설정 할 레벨(default : 1)</param>
    public void SetCharacterStat(EPlayerCharacterType characterType, int level = 1)
    {
        if (level <= 0) // level이 0 이하이면 1로 바꿈
        {
            level = 1;
        }
        this.level = level;

        if (characterType == EPlayerCharacterType.warrior)      // 전사의 Default 값을 설정
        {
            atk = 10 + (level * 10);            // 공격력 설정
            def = 10 + (level * 10);            // 방어력 설정
            moveSpeed = 1;                     // 이동 속도 설정
            attackSpeed = 1;                   // 공격 속도 설정
            hp = 100 + (level * 100);           // 체력 설정

        }
        else if (characterType == EPlayerCharacterType.archer)  // 궁수의 Default 값을 설정
        {
            atk = 20 + (level * 15);            // 공격력 설정
            def = 5 + (level * 10);             // 방어력 설정
            moveSpeed = 1;                      // 이동 속도 설정
            attackSpeed = 1;                   // 공격 속도 설정
            hp = 80 + (level * 80);             // 체력 설정
        }
        else
        {
            Debug.LogWarning("캐릭터의 Default 수치를 설정하지 못했습니다.");
        }
    }

    /// <summary>
    /// 캐릭터 얼굴 이미지를 바꾼다.
    /// </summary>
    /// <param name="characterType">바꿀 캐릭터 타입</param>
    public void ChangeFaceImage(EPlayerCharacterType characterType)
    {
        switch (characterType)
        {
            case EPlayerCharacterType.archer:
                characterFaceIcon = Resources.Load<Sprite>(PathOfResources.archarFaceImage);
                break;
            case EPlayerCharacterType.warrior:
                characterFaceIcon = Resources.Load<Sprite>(PathOfResources.warriorFaceImage);
                break;
            case EPlayerCharacterType.none:
                break;
            default:
                Debug.Assert(false);
                break;
        }
    }

    /// <summary>
    /// 게임에서 사용 할 캐릭터의 데이터를 반환한다.
    /// </summary>
    public UserData GetInGameCharacterData()
    {
        UserData data = new UserData();
        data.CopyData(this);
        return data;
    }
}



// TODO (장현명) : 내부에 데이터를 저정하는 부분을 쓸지 안쓸지 결정 후 처리해야 함
/// <summary>
/// 유저의 기본 데이터를 앱 내에서 불러와 플레이어 데이터에 저장한다.
/// </summary>
//private void LoadUserDataFromApplication()
//{
//    atk = PlayerPrefs.GetFloat("atk");
//    attackSpeed = PlayerPrefs.GetFloat("attackSpeed");
//    characterType = (EPlayerCharacterType)PlayerPrefs.GetInt("characterType");
//    //userData.consumableItems
//    crystal = PlayerPrefs.GetInt("crystal");
//    def = PlayerPrefs.GetFloat("def");
//    //userData.equipmentItemInUse
//    //userData.equipmentItems
//    exp = PlayerPrefs.GetFloat("exp");
//    gold = PlayerPrefs.GetInt("gold");
//    hp = PlayerPrefs.GetFloat("hp");
//    id = PlayerPrefs.GetString("id");
//    level = PlayerPrefs.GetInt("level");
//    moveSpeed = PlayerPrefs.GetFloat("moveSpeed");
//    name = PlayerPrefs.GetString("name");
//    Stamina = PlayerPrefs.GetInt("stamina");
//}
