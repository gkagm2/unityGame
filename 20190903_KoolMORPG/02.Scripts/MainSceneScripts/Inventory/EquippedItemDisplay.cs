using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 착용된 아이템의 UI
/// </summary>
public class EquippedItemDisplay : MonoBehaviour
{
    public EEquipmentItemType equippedItemType; // 착용된 장비 아이템의 타입
    [HideInInspector]
    public EquipmentItem card;                  // 장비 아이템 데이터

    public Text reinforcementText;
    public Image itemImage;
    public Text nameText;
    public Text tierText;

    public Image cardBackgroundImage;

    private void Awake()
    {
        card = new EquipmentItem();
        cardBackgroundImage = GetComponent<Image>();
        card.Reset();
    }

    /// <summary>
    /// UI를 업데이트 한다
    /// </summary>
    public void UpdateUI()
    {
        if(card.type == EEquipmentItemType.none)
        {
            gameObject.SetActive(false);
        }
        reinforcementText.text = card.reinforcementCount.ToString();
        itemImage.sprite = card.Icon;
        nameText.text = card.name;
        tierText.text = card.tier.ToString();

        // Tier에 따라서 아이템 카드의 색상을 변경한다.
        switch (card.tier)
        {
            case ETierType.old:
                cardBackgroundImage.color = ColorData.oldColor;
                break;
            case ETierType.normal:
                cardBackgroundImage.color = ColorData.normalColor;
                break;
            case ETierType.unique:
                cardBackgroundImage.color = ColorData.uniqueColor;
                break;
            case ETierType.legend:
                cardBackgroundImage.color = ColorData.legendColor;
                break;
            default:
                Debug.LogWarning("컬러 범위를 벗어남." + "name : "+ card.name + ", tier : " + card.tier.ToString());

                break;
        }
    }
}