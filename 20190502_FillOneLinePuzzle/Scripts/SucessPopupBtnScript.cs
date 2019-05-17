using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SucessPopupBtnScript : MonoBehaviour {
    
    //public int btnNumber;
    void Start()
    {
        
    }
    public void SuccessPopup_OnClickHomeBtn()
    {
        ScreenManager screenManager = GameObject.Find(MyPath.gameScreen).GetComponent<ScreenManager>();
        if (screenManager)
        {
            screenManager.SuccessPopup_OnClickMainMenuBtn(1);
        }
    }
}
