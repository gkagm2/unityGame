using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour {

    // Main button control
    public Camera camera;
    public GameObject mainScreen;
    public GameObject[] levelScreen;

    public GameObject[] stages;

    public GameObject gameScreen;
    short screenNumber;

    GameObject currentScreen;
    public  class LevelStageInfo
    {
        string level;
        string stage;
    }
    LevelStageInfo levelStageInfo;
	// Use this for initialization
	void Start () {
        currentScreen = mainScreen;
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    public void OnClickBackbtn()
    {
        currentScreen.SetActive(false);
        mainScreen.SetActive(true);
    }

    //Main Button Control
    public void MainOnClickLevelsBtn()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        
        for (int i = 0; i < levelScreen.Length; i++)
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name.Equals("LevelBtn" + (i+1)))
                {
                    
                    mainScreen.SetActive(false);
                    currentScreen = levelScreen[i];
                    levelScreen[i].SetActive(true);
                }
            }
        }
    }

    // Level 스크린에서 Stage버튼을 눌렀을 경우.
    public void LevelOnClickStagesBtn()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit)) //레이캐스트를 쏴서 충돌시킴.
        {
            Debug.Log(hit.collider.name);
            for(int i=0; i< 100; i++) //모든 스테이지를 검색한다. 
            {
                
                if (hit.collider.name.Equals("Stage (" + i + ")")) //스테이지의 이름과 같으면
                {
                    levelScreen[screenNumber].SetActive(false); //현재 레벨 스크린을 사라지게 하고
                    currentScreen = gameScreen;
                    gameScreen.SetActive(true); //게임 스크린을 킨다. 
                }
            }
            
        }
    }
}
