using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageScreen_CurrentLevelStarsScript : MonoBehaviour {
    [Tooltip("플레이어의 정보")]
    public PlayerInfo playerInfo;
    // Use this for initialization
    void Start () {
        // 해당 레벨에 대한 별의 개수를 가져온다.
        GetComponent<UILabel>().text = playerInfo.currentHaveStarsPerLevel[playerInfo.currentLevel-1].ToString() + "/" + playerInfo.maxStarsPerLevel[playerInfo.currentLevel-1].ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
