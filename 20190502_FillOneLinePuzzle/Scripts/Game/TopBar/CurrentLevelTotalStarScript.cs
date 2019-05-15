using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class CurrentLevelTotalStarScript : MonoBehaviour {
    [Tooltip("플레이어의 정보")]
    public PlayerInfo playerInfo;

    [Tooltip("나의 레벨 정보")]
    string levelName;
    public int myLevelNumber;

    // Use this for initialization
    void Start () {

        //TODO : parent가 3번이나 쓰는게 마음에 안듬
        levelName = transform.parent.parent.parent.name; // 현재 레벨의 이름을 가져온다.
        myLevelNumber = int.Parse(Regex.Replace(levelName, @"\D", "")); //레벨의 이름에서 숫자만 추출한다.
        
        // 레벨의 별의 개수와 레벨의 총 얻을 수 있는 별의 개수를 UILabel.text로 표시
        GetComponent<UILabel>().text = playerInfo.currentHaveStarsPerLevel[myLevelNumber-1].ToString() + "/" + playerInfo.maxStarsPerLevel[myLevelNumber-1].ToString();
    }
    private void Update()
    {
        // 레벨의 별의 개수와 레벨의 총 얻을 수 있는 별의 개수를 UILabel.text로 표시
        GetComponent<UILabel>().text = playerInfo.currentHaveStarsPerLevel[myLevelNumber - 1].ToString() + "/" + playerInfo.maxStarsPerLevel[myLevelNumber - 1].ToString();
    }
}
