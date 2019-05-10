using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class MapInfo : MonoBehaviour {
    [Tooltip("맵의 레벨 정보")]
    public short myLevel; // 레벨 정보

    [Tooltip("맵의 스테이지 정보")]
    public short myStage; // 스테이지 정보
    void Start()
    {
        string LevelAndStageName = gameObject.name;

        LevelAndStageName = LevelAndStageName.Substring(2); //두번째 자리까지 제거함. (LS 제거)
        string[] splitLevelAndStage = LevelAndStageName.Split('_');
        myLevel = short.Parse(splitLevelAndStage[0]); // 첫 번째 자리 (Level)을 myLevel에 대입
        myStage = short.Parse(splitLevelAndStage[1]); // 두 번째 자리 (Stage)를 myStage에 대입
        Debug.Log("myLevel : " + myLevel + "," + " myStage : " + myStage);
    }
}
