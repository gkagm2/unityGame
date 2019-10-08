using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 메일박스 카드 UI에 관한 클래스
/// </summary>
public class MailBoxItemDisplay : MonoBehaviour
{
    //equipmentItem,
    //gold,
    //crystal,
    //stat
    ERewardType rewardType;             // 보상 타입

    public Image itemImage;             // 이미지
    public Text descriptionText;        // 설명 창
    public MailBoxPopupManager mailBoxPopupManager;
    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        //TODO : 어떻게 구조를 짤 것인가
        //descriptionText.text = ;
        //itemImage.sprite = ;
    }

    /// <summary>
    /// 받기 버튼 클릭 시 호출
    /// </summary>
    public void OnClick_GetButton(int type)
    {
        if(type == 1)
        {
            PopupManager.instance.OpenAlarmPopup("크리스탈 50개를 선물받았습니다.");
            mailBoxPopupManager.TestReceive(1);
        }
        else
        {
            PopupManager.instance.OpenAlarmPopup("크리스탈 20개, 1000 골드를 선물받았습니다.");
            mailBoxPopupManager.TestReceive(2);
        }
        gameObject.SetActive(false);
    }
}
