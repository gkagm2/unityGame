using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessPopupBtnClickScript : MonoBehaviour {
    public int popupNumber;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnClick_HomeBtn()
    {
        ScreenManager screenManager = GameObject.Find(MyPath.gameScreen).GetComponent<ScreenManager>();
        if (screenManager)
        {
            screenManager.SuccessPopup_OnClickMainMenuBtn(popupNumber);
        }
    }
}
