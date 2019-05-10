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

    LevelStageInfo[] levelStageInfo;
    

    //Stage와 Level
    public short currentLevel = 1;
    public short currentStage = 1;

    // 불러온 모든 레벨의 모든 스테이지의 총 개수
    int allStagesCount = 0;

    // Use this for initialization
    void Start () {
        // 별을 얻을 수 있는 최대 개수 구하기
        maxStars = GetSumInArray(maxStarsPerLevel); //배열 안에 있는 값을 모두 더하여 리턴
        Debug.Log("별 최대 개수 : " + maxStars);

        // 모든 레벨에서 가지고 있는 별들을 합친 개수 구하기
        currentHaveStarsTotalNumber = GetSumInArray(currentHaveStarsPerLevel); //배열 안에 있는 값을 모두 더하여 리턴
        Debug.Log("별 합친 개수 :  " + currentHaveStarsTotalNumber);
        

        // 맵을 가져오는 부분
        allStagesCount = GetAllStagesCount(levelCount); //총 스테이지의 개수를 리턴한다.
        Debug.Log("count :  " + allStagesCount);
        levelStageInfo = new LevelStageInfo[allStagesCount];
        LevelStageInit(); //레벨과 스테이지를 초기화한다.
        
        for(int i=0;i< levelStageInfo.Length; i++)
        {
            Debug.Log(levelStageInfo[i].level);
            Debug.Log(levelStageInfo[i].stage);
        }


        //TODO : 맵을 StageScreen에 세팅하는 부분

        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // 레벨과 스테이지를 초기화한다.
    public void LevelStageInit()
    {
        Debug.Log("LeveStageInfo length :  " + levelStageInfo.Length);
        GetLevelStageObject(); //레벨스테이즈 오브젝트를 얻는다.

        //레벨과 스테이지를 초기화한다.
        for (int i = 0; i < levelStageInfo.Length; i++) 
        {
            levelStageInfo[i].level = levelStageInfo[i].obj.GetComponent<MapInfo>().myLevel; //레벨을 얻는다.
            levelStageInfo[i].level = levelStageInfo[i].obj.GetComponent<MapInfo>().myStage; //스테이지를 얻는다.
            if(levelStageInfo[i].obj.activeSelf == true) //Map이 set 되어있으면 true
            {
                levelStageInfo[i].isSuccess = true; 
            }
            else //Map이 set 되어있지 않으면 false
            {
                levelStageInfo[i].isSuccess = false;
            }
        }
        
    }

    // 레벨스테이지 오브젝트를 얻는다.
    public void GetLevelStageObject()
    {
        Debug.Log("길이 : " + allStagesCount);
        GameObject[] mapObj = new GameObject[allStagesCount]; // 
        Debug.Log("mapObj의 길이1 : " + mapObj.Length);
        mapObj = GameObject.FindGameObjectsWithTag("Map");
        Debug.Log("mapObj의 길이2 : " + GameObject.FindGameObjectsWithTag("Map").Length);
        Debug.Log("mapObj의 길이2 : " + mapObj.Length);
        for(int i = 0; i < levelStageInfo.Length; i++)
        {
            levelStageInfo[i].obj = mapObj[i]; // 오브젝트를 대입
            
        }
    }


    // 스테이지의 개수를 가져온다.
    public int GetAllStagesCount(int level)
    {
        //자식 오브젝트들의 개수
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
