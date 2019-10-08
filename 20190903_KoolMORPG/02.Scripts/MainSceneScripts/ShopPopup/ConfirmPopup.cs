using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmPopup : MonoBehaviour
{
    public Text itemPackNameReconfirmPopup;
    public Text itemPackNameConfirmPopup;

    /// <summary>
    /// 재확인 아이템 텍스트와 확인 아이템 텍스트의 동일화
    /// </summary>
    public void NameMatch()
    {
        itemPackNameConfirmPopup.text = itemPackNameReconfirmPopup.text + "을(를) 구매 했습니다.";
    }
}