using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 모든 Scene에서 사용하는 팝업
/// </summary>
public partial class PopupManager : MonoBehaviour
{
    #region Singleton
    public static PopupManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    } 
    #endregion

    [Header("All scene")]
    public GameObject alarmPopup;
    public GameObject standByPopup;
    public Text alarmText;

    public void Start()
    {
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
    /////////////// Alarm popup ////////////////

    /// <summary>
    /// 알람 팝업 창 On/Off
    /// </summary>
    /// <param name="isOpen">열지 닫을지 선택하는 값</param>
    public void OpenAlarmPopup(bool isOpen)
    {
        if(isOpen)
        {
            alarmPopup.SetActive(true);
        }
        else
        {
            alarmText.text = "";
            alarmPopup.SetActive(false);
        }
    }

    /// <summary>
    /// 알람 팝업 창 열기 (알람 글을 입력할 수 있다.)
    /// </summary>
    /// <param name="title">알람 글</param>
    public void OpenAlarmPopup(string title)
    {
        alarmText.text = title;
        OpenAlarmPopup(true);
    }

    /////////////// Stand by popup //////////////////
    
    /// <summary>
    /// 비동기 서버 통신 중 아무것도 클릭 할 수 없게 할 때 사용하는 팝업 On/Off
    /// </summary>
    /// <param name="isOpen">열지 닫을지 선택하는 값</param>
    public void OpenStandByPopup(bool isOpen)
    {
        if (isOpen)
        {
            standByPopup.SetActive(true);
        }
        else
        {
            standByPopup.SetActive(false);
        }
    }
}

/// <summary>
/// Loading Scene에서 사용하는 팝업
/// </summary>
public partial class PopupManager : MonoBehaviour
{
    [Header("Loading scene")]
    public GameObject connectionFailPopupOrNull;

    /// <summary>
    /// 연결 실패 팝업 창 On/Off
    /// </summary>
    /// <param name="isOpen">연결 실패 팝업 창</param>
    public void OpenConnectionFailPopup(bool isOpen)
    {
        if (connectionFailPopupOrNull == null)
        {
            return;
        }

        if (isOpen)
        {
            connectionFailPopupOrNull.SetActive(true);
        }
        else
        {
            connectionFailPopupOrNull.SetActive(false);
        }
    }
}

/// <summary>
/// Main Scene에서 사용하는 팝업
/// </summary>
public partial class PopupManager : MonoBehaviour
{
    [Header("Main scene")]
    public GameObject itemSaleConfirmPopupOrNull;


    /////////// Item sale confirm popup ////////////

    /// <summary>
    /// 아이템 판매 확인 팝업 창 On/Off
    /// </summary>
    /// <param name="isOpen">열지 닫을지 선택하는 값</param>
    public void OpenItemSaleConfirmPopup(bool isOpen)
    {
        if (itemSaleConfirmPopupOrNull == null)
        {
            return;
        }

        if (isOpen)
        {
            itemSaleConfirmPopupOrNull.SetActive(true);
            itemSaleConfirmPopupOrNull.GetComponent<ItemSaleConfirmPopup>().UpdateUI();
        }
        else
        {
            itemSaleConfirmPopupOrNull.SetActive(false);
        }
    }
}