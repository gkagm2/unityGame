using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentTotalStarScript : MonoBehaviour {
    [Tooltip("플레이어의 정보")]
    public PlayerInfo playerInfo;
    
	// Use this for initialization
	void Start () {
        // 현재까지 누적된 별의 개수 출력
        GetComponent<UILabel>().text = playerInfo.currentHaveStarsTotalNumber.ToString() + "/" + playerInfo.maxStars.ToString();
    }
    void Update()
    {
        // 현재까지 누적된 별의 개수 출력
        GetComponent<UILabel>().text = playerInfo.currentHaveStarsTotalNumber.ToString() + "/" + playerInfo.maxStars.ToString();
    }
}
