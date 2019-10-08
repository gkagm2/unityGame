using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEquipmentItemCard", menuName = "EquipmentItem")]
public class EquipmentItemData : ScriptableObject
{
    public new string name;                 // Item name
    public EEquipmentItemType type;
    public int imageNumber;             // 0에서 시작

    /// <summary>
    /// 데이터를 복사한다.
    /// </summary>
    /// <param name="data">복사할 데이터</param>
    public void CopyData(EquipmentItemData data)
    {
        name = data.name;
        type = data.type;
        imageNumber = data.imageNumber;
    }
}
