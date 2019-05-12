using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class StageInfo : MonoBehaviour {

    
    public short myLevel;
    public short myStage;
    public GameObject currentStageNumber;   // 현재 스테이지의 번호
    public GameObject coinPresentImage;     // 선물박스 이미지
    public GameObject backgroundLock;       // 스테이지가 잠긴 이미지
    public GameObject backgroundUnLock;     // 스테이지가 풀린 이미지
    public GameObject backgroundNext;       // 깨야할 스테이지 이미지

    //TODO : 이거 해야 하는데..?
    public UIButton myBtn;                  // 자기 자신을 클릭했을 때의 버튼

    public PlayerInfo playerInfo;           // 플레이어 정보

    public int presentStage = 20;           // 선물을 받을 수 있는 스테이지
	// Use this for initialization
	void Start () {
        playerInfo = GameObject.Find("UI Root/Screens/").GetComponent<PlayerInfo>(); // 플레이어 정보를 가져옴
        if (!playerInfo)
        {
            Debug.Log("PlayerInfo 못찾음");
        }

        //TODO 버튼 클릭 어떻게 하냐.
        //myBtn.onClick = 
        CheckLevelAndStage(); // 레벨과 스테이지를 체크한다.
    }
	
	// Update is called once per frame
	void Update () {
        CheckLevelAndStage(); // 레벨과 스테이지를 체크한다.
    }

    public void Click()
    {
        Debug.Log("클릭했음" + myLevel + ", " + myStage);
        playerInfo.currentStage = myStage; // 스테이지를 현재 스테이지로 집어넣는다.
    }
    
    // 레벨과 스테이지를 체크한다.
    public void CheckLevelAndStage()
    {

        //Debug.Log("playerInfo.currentLevel : " + playerInfo.currentLevel + "playerInfo.currentStage : " + playerInfo.currentStage);


        //for (int i = 0; i < playerInfo.levelStageInfo.Length; i++)
        //{
        //    Debug.Log("player의 레벨 : " + playerInfo.levelStageInfo[i].level + " player의 stage : " + playerInfo.levelStageInfo[i].stage);
        //}

        LevelStageInfo myLevelStageInfo;  // 레벨과 스테이지의 정보를 불러옴.

        // player 정보를 찾는다.
        for(int i=0; i< playerInfo.levelStageInfo.Length; i++)
        {
            Debug.Log("playerInfo. level : " + playerInfo.levelStageInfo[i].level);
            if(myLevel == playerInfo.levelStageInfo[i].level && myStage == playerInfo.levelStageInfo[i].stage) // 스테이지 정보의 레벨과 스테이지와 PlayerINfo의 레벨과 스테이지가 같다면
            {
                Debug.Log("찾았다 myLevel : " + myLevel + ", " + myStage);
                myLevelStageInfo = playerInfo.levelStageInfo[i]; // 레벨과 스테이지 정보를 가져오고.
                SettingLevelAndStageInfo(); /// 레벨과 스테에지의 정보를 설정한다.
                ChangeUIImage(myLevelStageInfo); //이미지의 상태를 바꾼다.
               
            }
        }
    }

    // 레벨과 스테이지의 정보를 설정한다.
    public void SettingLevelAndStageInfo()
    {
        Debug.Log("스테이지 : " + myStage);
        currentStageNumber.GetComponent<UILabel>().text = myStage.ToString();
    }

    // 풀린 상태로 이미지를 바꿈
    public void SetUnLockState()
    {
        currentStageNumber.SetActive(true); // 0현재 스테이지의 번호를 보이게 한다.
        backgroundUnLock.SetActive(true); // 스테이지가 풀린 이미지를 보이게 한다.
        coinPresentImage.SetActive(false); // 선물박스 이미지를 안보이게 한다.
        backgroundLock.SetActive(false); // 잠김 이미지를 안보이게 한다.
    }

    // 잠긴 상태로 이미지를 바꿈
    public void SetLockState()
    {
        currentStageNumber.SetActive(true); // 현재 스테이지의 번호를 보이게 한다.
        backgroundUnLock.SetActive(false); //스테이지가 풀린 이미지를 안보이게 한다.
        if (myStage % presentStage == 0) //선물을 받을 수 있는 스테이지면
        {
            coinPresentImage.SetActive(true); // 선물 상자가 보임
        }
        else // 아니면
        {
            coinPresentImage.SetActive(false); // 선물 상자가 안보임
        }
        backgroundLock.SetActive(true); //잠긴 이미지를 보이게 한다.
    }

    // 이미지의 상태를 바꾼다.
    public void ChangeUIImage(LevelStageInfo myLevelStageInfo)
    {

        if (myLevelStageInfo.isSuccess == true) // 클리어 한 상태면
        {
            SetUnLockState(); //잠긴 상태로 바꿈
        }
        else if (myLevelStageInfo.isSuccess == false) // 클리어 하지 않은 상태면
        {
            SetLockState(); // 풀린 상태로 바꿈
        }
    }
}
