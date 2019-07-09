using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StateBar : MonoBehaviour {
    public GameObject optionPopup;
    


    // -------- StateBar --------

    // option 열고 닫기.
    public void OnClick_OptionPopupBtn(bool openFlag) //true 열기, false 닫기
    {
        if (openFlag)
            optionPopup.SetActive(true);
        else
            optionPopup.SetActive(false);
    }
}
