using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopPopupManager : MonoBehaviour
{
    #region Singleton
    public static TopPopupManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    } 
    #endregion

    public TopPopup[] topPopups;
    public GameObject mailBoxPopup;    
    public GameObject optionPopup;

    public GameObject characterObj; // 캐릭터 Object

    /// <summary>
    /// 모든 Top popup의 UI를 업데이트한다.
    /// </summary>
    public void TopPopupsUpdate()
    {
        for (int i = 0; i < topPopups.Length; ++i)
        {
            topPopups[i].UpdateUI();
        }
    }

    /// <summary>
    /// MailBox 팝업 On/Off
    /// </summary>
    /// <param name="isOpen"></param>
    public void OpenMailBoxPopup(bool isOpen)
    {
        if (isOpen)
        {
            mailBoxPopup.SetActive(true);
            characterObj.SetActive(false);
        }
        else
        {
            mailBoxPopup.SetActive(false);
            characterObj.SetActive(true);
        }
    }

    /// <summary>
    /// 옵션 팝업 On/Off
    /// </summary>
    // TODO (신민승) : 옵션 팝업 연동하기
    public void OpenOptionPopup(bool isOpen)
    {
        if (isOpen)
        {
            optionPopup.SetActive(true);
            characterObj.SetActive(false);
        }
        else
        {
            optionPopup.SetActive(false);
            characterObj.SetActive(true);
        }
    }
}
