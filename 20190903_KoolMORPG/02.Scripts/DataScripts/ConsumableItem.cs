using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 소비용 아이템의 데이터 정보를 담고있는 클래스
/// </summary>
public class ConsumableItem
{
    public int id;                      // Item id;
    public string name;                 // Item name
    public int gold;
    public EConsumableItemType type;
    public int imageNumber;
    public Sprite Icon {
        get
        {
            string path = "";
            switch (type)
            {
                case EConsumableItemType.hpPotion:
                    path = PathOfResources.consumableImages + "hpPotion";
                    break;
            }
            return Resources.Load<Sprite>(path);
        }
    }             // Item image
    /// <summary>
    /// 값을 복사한다.
    /// </summary>
    /// <param name="data">복사 할 소비 아이템</param>
    public void CopyData(ConsumableItem data)
    {
        id = data.id;
        name = data.name;
        gold = data.gold;
        type = data.type;
        imageNumber = data.imageNumber;
    }

    /// <summary>
    /// 현재 클래스의 데이터 값들을 복사한 데이터를 반환한다.
    /// </summary>
    /// <returns>반환할 데이터</returns>
    public ConsumableItem GetCopiedData()
    {
        ConsumableItem data = new ConsumableItem();
        data.CopyData(this);
        return data;
    }
}
