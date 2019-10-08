using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 인벤토리 아이템 카드의 정보를 보여주는 클래스
/// </summary>
public class InventoryItemDisplay : MonoBehaviour
{
    public EquipmentItem card;
    public Text reinforcementText;  // 강화 횟수
    public Image icon;              // 아이템 이미지
    public Text itemName;           // 아이템 이름
    public Text tier;               // 아이템 등급

    private Image cardBackgroundImage;        // 카드의 이미지

    private void Awake()
    {
        card = new EquipmentItem();
        cardBackgroundImage = GetComponent<Image>();
    }
    /// <summary>
    /// UI를 업데이트 한다.
    /// </summary>
    public void UpdateUI()
    {
        reinforcementText.text = card.reinforcementCount.ToString();
        icon.sprite = card.Icon;
        itemName.text = card.name;
        tier.text = card.tier.ToString();

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
                cardBackgroundImage.color = Color.blue;
                Debug.Assert(false, "컬러 범위를 벗어남!");
                break;
        }
    }

    /// <summary>
    /// 아이템 카드버튼을 눌렀을 때 호출 된다.
    /// </summary>
    public void OnClick_ItemCardBtn()
    {
        DetailItemCardPopup detailItemCardPopup = GetComponentInParent<DetailItemCardPopup>();
        detailItemCardPopup.choicedItemCardObj = gameObject;
        detailItemCardPopup.OpenDetailItemCardPopup(true);
    }
}
