using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class StageInfo : MonoBehaviour {

    
    public int myLevel;
    public int myStage;
    public GameObject currentStageNumber;   // 현재 스테이지의 번호
    public GameObject coinPresentImage;     // 선물박스 이미지
    public GameObject backgroundLock;       // 스테이지가 잠긴 이미지
    public GameObject backgroundUnLock;     // 스테이지가 풀린 이미지
    public GameObject backgroundNext;       // 깨야할 스테이지 이미지

    //TODO : 이거 해야 하는데..?
    public UIButton myBtn;                  // 자기 자신을 클릭했을 때의 버튼


	// Use this for initialization
	void Start () {

        Debug.Log(GameObject.Find(Path.gameScreen_Maps));
        

        //TODO 버튼 클릭 어떻게 하냐.
        //myBtn.onClick = 
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Click()
    {
        Debug.Log("클릭했음");
    }
}
