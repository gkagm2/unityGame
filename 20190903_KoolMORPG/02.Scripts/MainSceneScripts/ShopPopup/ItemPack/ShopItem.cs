using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewShopItemCard", menuName = "ShopItem")]
public class ShopItem : ScriptableObject
{
    // 데이터
    public string title;               // 제목
    public Sprite itemIcon;            // 아이템 이미지
    public int cost;                   // 
    public Sprite costIcon;            // 화폐 이미지
    public int purchaseCount;          // 구매 개수
    public EShopItemType shopItemType; // 판매 타입
}
