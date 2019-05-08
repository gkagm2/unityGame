using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinLabelScript : MonoBehaviour {
    [Tooltip("플레이어의 정보")]
    public PlayerInfo playerInfo;

    void Start()
    {
        GetComponent<UILabel>().text = playerInfo.coin.ToString();
    }
}
