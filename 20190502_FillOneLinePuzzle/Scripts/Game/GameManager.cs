using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerInfo playerInfo;
    public GameObject[] levelObjs;
    void Start()
    {
        Debug.Log("GameManager.cs 실행 됨");
        playerInfo = GetComponent<PlayerInfo>(); // 플레이어 정보 가져옴
        levelObjs = new GameObject[playerInfo.GetLevelCount()];

        // 플레이어 Score를 가져옴
        for(int i=0; i <levelObjs.Length; i++)
        {
            Debug.Log("플레이어 정보를 가져옴");
            levelObjs[i] = GameObject.Find(Path.levelScreen_Level + (i+1) + "/Score/");
            
            if (levelObjs[i])
            {
                
                Debug.Log("levelObj[i] :" + i + ", " + levelObjs[i].name + "을 가져옴");
            }
            else
            {
                Debug.Log("Level Object를 못찾음 :" + levelObjs[i]);
            }
        }
    }

    void Update()
    {
        MakeStateUnLockLevel();
    }


    // 해제 상태로 만들기
    public void MakeStateUnLockLevel()
    {


        for(int i=0;i  < playerInfo.GetLevelCount() - 1; i++)
        {
            // 각 레벨의 별에 max값을 얻은 값이 같으면
            if(playerInfo.maxStarsPerLevel[i] == playerInfo.currentHaveStarsPerLevel[i])
            {
                Debug.Log("레벨 : " + i + "가 성공되었으면" + levelObjs[i] + ", " + levelObjs[i+1]);
                // 위에 레벨의 lock을 unlock으로 바꾼다.
                GameObject tempLockobj = levelObjs[i + 1].transform.Find("Lock").gameObject; // 잠금을 숨기고
                if (tempLockobj)
                {
                    tempLockobj.SetActive(false);
                }
                else
                {
                    Debug.Log("못찾음! lock");
                }
                
                GameObject tempUnLockObj = levelObjs[i + 1].transform.Find("Unlock").gameObject;// 풀림을 보여줌
                if (tempUnLockObj)
                {
                    tempUnLockObj.SetActive(true);
                }
                else
                {
                    Debug.Log("못찾음! unlock");
                }
                Debug.Log("success unlock : " + (i+1) + " level");

                if(playerInfo.myProgressLevel == i) // 현재 진행 상태와 맞물리면
                {
                    playerInfo.LevelUnLockSetting(); // 잠금해제 세팅

                    ScreenManager screenManager = GetComponent<ScreenManager>();
                    screenManager.StageScreen_InitState(); //스테이지 스크린을 초기화한다.
                    
                }
            }
        }
    }

    //public int currentLevel; //현재 레벨 
    //public int currentStage; //현재 스테이지

    //public int currentPlayLevel; // 현재 플레이하는 레벨
    //// 맵 세팅 매니저
    //public MapDesign mapDesign;

    ////public MapSetting mapSetting;
    //// Use this for initialization
    //void Start()
    //{
    //    PlayerPrefs.SetInt("PlayerLevel", 2);
    //    PlayerPrefs.SetInt("PlayerStage", 20);
    //    currentLevel = 2;
    //    currentStage = 20;
    //    GetCurrentLevelAndStage(); // 이전에 했던 데이터를 가져와 레벨과 스테이지 초기화.
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}


    //// 맵사이즈 상태를 바꾼다.
    //public void ChangeMapSize()
    //{
    //    // Level이 1이면
    //    if(currentLevel + 1 == 1)
    //    {
    //        // 맵 사이즈 상태가  size4X4로 바꾼다.
    //        mapDesign.mapSizeState = MapDesign.MapSizeState.size4x4;
    //    }

    //    // Level이 2,3,4,5,6이면
    //    if(currentLevel + 1 >= 2 || currentLevel <= 6)
    //    {
    //        // 맵 사이즈 상태를 size5X5로 바꾼다.
    //        mapDesign.mapSizeState = MapDesign.MapSizeState.size4x4;
    //    }

    //    // Level이 7,8,9,10이면
    //    if(currentLevel + 1 >= 7 || currentLevel <= 10)
    //    {
    //        // 맵 사이즈 상태롤 size6X6으로 바꾼다.
    //        mapDesign.mapSizeState = MapDesign.MapSizeState.size4x4;
    //    }
    //    //mapSetting.MakeMapImage(); //맵 사이즈의 생성을 바꾼다
    //}

    //// 플레이어의 현재 상태까지 초기화
    //public void InitLevelStageUntilCurrentState()
    //{
    //    //LevelStageInfo.levelStage[] = GetCurrentLevelAndStage(); //플레이어의 레벨과 스테이지를 불러온다.
    //    // 레벨과 스테이지를 플레이어가 했었던 레벨과 스테이지까지 true로 초기화 

    //    // 이전부터 플레이어가 클리어했던 레벨과 스테이지까지 true로 초기화
    //    for (int i = 0; i < currentLevel ; i++) 
    //    {
    //        for (int j = 0; j < currentStage ; j++)
    //        {
    //            LevelStageInfo.levelStage[i, j] = true; //초기화 한다.
    //            //Debug.Log("level : " +i + " stage : " + j);
    //        }
    //    }
    //}

    //// 플레이어의 레벨과 스테이지의 상태를 불러온다.
    //public void GetCurrentLevelAndStage()
    //{
    //    currentLevel = PlayerPrefs.GetInt("PlayerLevel"); //플레이어의 레벨을 불러온다.
    //    currentStage = PlayerPrefs.GetInt("PlayerStage"); //플레이어의 스테이지를 불러온다. 
    //    //TODO
    //    //Debug.Log("currentLevel : " + currentLevel + " currentStage : " + currentStage); 
    //    LevelStageInfo.levelStage[currentLevel, currentStage] = true; // 플레이어가 했었던 레벨과 스테이지 상태를 true로 바꿈.
    //    InitLevelStageUntilCurrentState(); // 플레이어의 현재 상태까지 초기화
    //}
}
