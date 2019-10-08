public enum ETierType
{
    old = 1,
    normal,
    unique,
    legend
}

public enum ETendency
{
    aggressive,
    defensive,
    speedy
}

public enum EEquipmentItemType
{
    helmet,
    armor,
    shoes,
    weapon,
    none = 100
}

public enum EPlayerCharacterType
{
    none,
    warrior,
    archer
}

public enum EConsumableItemType
{
    hpPotion
}

public enum EStageLevel
{
    easy,
    normal,
    difficult
}

/// <summary>
/// 게임을 시작하거나 끝날 때 상태.
/// </summary>
public enum EClearState
{
    clear,
    fail,
    none
}

public enum ERewardType
{
    equipmentItem,
    gold,
    crystal,
    stat
}

public enum EShopItemType
{
    weapon,
    weaponPackage,
    defence,
    defencePackage,
    hpPotion,
    stamina,
    crystal,
    gold
}

///////////////  Server Client  ////////////

public enum ESaveLoadMode
{
    server,
    application
}

// 유저를 생성 할 때 GPGS ID를 이용하여 생성하는지, GPGS ID를 이용하지 않고 생성하는지 구분
public enum EUserCreateMode 
{
    withGPGSId,
    withoutGPGSId
}