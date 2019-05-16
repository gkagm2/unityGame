using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageScreen_CurrentLevelScript : MonoBehaviour {
    [Tooltip("플레이어의 정보")]
    public PlayerInfo playerInfo;
    // Use this for initialization
    void Start () {
        // 현재 레벨을 표시
        GetComponent<UILabel>().text = "Level " + playerInfo.currentLevel.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        // 현재 레벨을 표시
        GetComponent<UILabel>().text = "Level " + playerInfo.currentLevel.ToString();
    }
}
