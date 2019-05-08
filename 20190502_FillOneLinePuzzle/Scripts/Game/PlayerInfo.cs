using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {

    [Header("Player")]
    [Tooltip("현재 가지고 있는 코인")]
    public int coin;

    [Space]

    [Tooltip("얻을 수 있는 최대 별의 개수")]
    public int maxStar;

    [Space]

    [Tooltip("현재 가지고있는 별의 총 개수")]
    public int currentTotalStar;

    [Space]

    [Tooltip("레벨마다의 최대 별의 개수")]
    public int levelMaxStar;

    [Space]

    [Tooltip("각 레벨에 해당하는 별의 개수")]
    public int currentLevelTotalStar;

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
