using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonManager : MonoBehaviour {
    [Header("Main Panel")]
    public UIPanel firstPanel;
    public GameObject firstPanelObj;
    public bool flag;
    public UILabel label;
    public GameObject label2;

    [Header("Second Panel")]
    public UIPanel secondPanel;
    public GameObject secondPanelObj;

    //Main panel

    public void First_StartBtn()
    {
        if (flag == true)
        {
            flag = false;
            label.enabled = false;
        }
        else
        {
            flag = true;
            label.enabled = true;
        }
    }
    public void First_Start2Btn()
    {
        if (flag == true)
        {
            flag = false;
            label2.SetActive(false);
        }
        else
        {
            flag = true;
            label2.SetActive(true);
        }
    }

    public void First_PlayBtn()
    {
        secondPanel.enabled = true;
        firstPanel.enabled = false;
    }
    public void First_PlayObjBtn()
    {
        secondPanelObj.SetActive(true);
        firstPanelObj.SetActive(false);
            
    }


    //Second panel

    public void Second_BackBtn()
    {
        firstPanel.enabled = true;
        secondPanel.enabled = false;
    }
    public void Second_BackObjBtn()
    {
        firstPanelObj.SetActive(true);
        secondPanelObj.SetActive(false);
    }
    
}
