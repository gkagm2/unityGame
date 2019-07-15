using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumDefine{

}

public enum LevelStatus
{
    level1,
    level2,
    level3,
    level4,
    level5,
    level6,
    level7
};

public enum ActivateStatus
{
    Start,
    Stop
}

// TODO : enum 이름이 마음에 안듬......... 뭐라고 해야되냐
// 게임에서 이벤트가 발생한 상태
public enum GameEventOccurStatus
{
    GetCoin, // 코인을 얻었을 경우
    GetItem, // 아이템을 얻었을 경우
    UpdateUI, // 게임화면의 모든 UI 업데이트

}

public enum WhereGetCoinStatus
{
    InTheGame,
    InTheStore
}