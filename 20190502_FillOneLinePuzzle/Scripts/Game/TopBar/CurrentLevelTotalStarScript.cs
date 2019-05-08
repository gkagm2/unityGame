using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentLevelTotalStarScript : MonoBehaviour {
    [Tooltip("플레이어의 정보")]
    public PlayerInfo playerInfo;
    // Use this for initialization
    void Start () {
        Debug.Log(playerInfo.currentLevelTotalStar.ToString() + "/" + playerInfo.levelMaxStar.ToString());
        GetComponent<UILabel>().text = playerInfo.currentLevelTotalStar.ToString() + "/" + playerInfo.levelMaxStar.ToString();
    }
}
