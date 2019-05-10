using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentLevelStageScript : MonoBehaviour {
    [Tooltip("플레이어의 정보")]
    public PlayerInfo playerInfo;

	// Use this for initialization
	void Start () {
        //현재 레벨과 스테이지를 보여줌.
        GetComponent<UILabel>().text = "Level " + playerInfo.currentLevel.ToString() + "-" + playerInfo.currentStage.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
