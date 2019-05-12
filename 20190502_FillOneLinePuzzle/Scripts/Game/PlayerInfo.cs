using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerInfo : MonoBehaviour {

    public int levelCount = 0;
    public int stageCount = 0;

    [Header("Player")]
    [Tooltip("현재 가지고 있는 코인")]
    public int coin = 200;

    [Space]

    [Tooltip("얻을 수 있는 최대 별의 개수")]
    public int maxStars = 0;

    [Space]

    [Tooltip("현재 가지고있는 별의 총 개수")]
    public int currentHaveStarsTotalNumber = 0;

    [Space]

    [Tooltip("레벨마다의 최대 별의 개수")]
    public int[] maxStarsPerLevel;

    [Space]

    [Tooltip("현재 가지고 있는 각 레벨에 해당하는 별의 개수")]
    public int[] currentHaveStarsPerLevel;
    

    public LevelStageInfo[] levelStageInfo; // 각 레벨과 스테이지의 정보를 담고있다.
    


    //Stage와 Level
    public short currentLevel = 1;
    public short currentStage = 1;

    // 불러온 모든 레벨의 모든 스테이지의 총 개수
    int allStagesCount = 0;

    // Use this for initialization
    void Start () {
        Debug.Log("PlayerInfo.cs 스크립트 시작");


        LevelStageInfoSetting(); // 레벨과 스테이지의 정보를 세팅한다.





        // 별을 얻을 수 있는 최대 개수 구하기
        maxStars = GetSumInArray(maxStarsPerLevel); //배열 안에 있는 값을 모두 더하여 리턴
        Debug.Log("별 최대 개수 : " + maxStars);

        // 모든 레벨에서 가지고 있는 별들을 합친 개수 구하기
        currentHaveStarsTotalNumber = GetSumInArray(currentHaveStarsPerLevel); //배열 안에 있는 값을 모두 더하여 리턴
        Debug.Log("별 합친 개수 :  " + currentHaveStarsTotalNumber);
        

        

        
    }

    
	
	// Update is called once per frame
	void Update () {
		
	}


    // 레벨과 스테이지의 정보를 세팅한다.
    public void LevelStageInfoSetting()
    {

        GameObject[] maps = new GameObject[GetAllStagesCount()];
        maps = GameObject.FindGameObjectsWithTag("Map"); // 존재 하고 있는 스테이지의 GameObject를 가져옴.
        for(int i = 0; i < maps.Length; i++)
        {
            Debug.Log("name : " + maps[i].name + "level : " + maps[i].GetComponent<MapInfo>().myLevel + " stage : " + maps[i].GetComponent<MapInfo>().myStage);

        }
        levelStageInfo = new LevelStageInfo[maps.Length]; //존재하고 있는 스테이지의 개수만큼 동적으로 할당받는다.
        Debug.Log("levelStageInfo 할당 개수 : " + levelStageInfo.Length);


        // TODO : Test 삭제하셈
        levelStageInfo[0] = new LevelStageInfo();
        levelStageInfo[0].isSuccess = true;
        levelStageInfo[0].level = maps[0].GetComponent<MapInfo>().myLevel;
        levelStageInfo[0].stage = maps[0].GetComponent<MapInfo>().myStage;
        levelStageInfo[0].obj = maps[0];        // leveStageInfo를 값 설정을 해준다.

        //Test i는 0으로 바꾸셈
        for (int i=1; i < levelStageInfo.Length; i++)
        {
            levelStageInfo[i] = new LevelStageInfo();
            levelStageInfo[i].isSuccess = false;
            levelStageInfo[i].level = maps[i].GetComponent<MapInfo>().myLevel;
            levelStageInfo[i].stage = maps[i].GetComponent<MapInfo>().myStage;
            levelStageInfo[i].obj = maps[i];
        }
    }


    // 스테이지의 개수를 가져온다.
    public int GetAllStagesCount()
    {
        //자식 오브젝트들의 개수를 구한다
        int count = GameObject.Find(Path.gameScreen_Maps).gameObject.transform.childCount;

        // TODO : 해당 레벨의 자식 오브젝트 수를 구하는 것을 구현.
        return count;

    }


    //배열 안에 있는 값을 모두 더하기
    public int GetSumInArray(int[] array)
    {
        int sum = 0;
        for(int i = 0; i < array.Length; i++)
        {
            sum += array[i];
        }
        return sum;

    }

}
