using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInfo : MonoBehaviour {

    public Image hpImg;
    public Image mpImg;

    int currentHp;
    int currentMp;
    public int hp = 10;
    public int mp = 10;


	// Use this for initialization
	void Start () {
        InitPlayerInfo();
        SettingPlayerUI();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InitPlayerInfo()
    {
        currentHp = hp;
        currentMp = mp;
        // call the data of player information (json or scv file)
    }

    public void SettingPlayerUI()
    {
        
        hpImg.fillAmount = currentHp / (float)hp;
        mpImg.fillAmount = currentMp / (float)mp;
    }

    
}
