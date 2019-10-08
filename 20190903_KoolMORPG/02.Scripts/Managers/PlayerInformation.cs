using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System;

/// <summary>
/// 플레이어의 데이터를 담고 있는 클래스
/// </summary>
public static class PlayerInformation
{
    public static UserData userData;                 // 캐릭터의 정보가 담겨있다.
    public static UserInventory inventory;           // 장비, 소비 아이템들의 정보가 담겨있다.
    public static StageData stageData;               // 스테이지의 정보가 담겨있다.
    // TODO (장현명) : 메일 데이터를 가져오는 리스트를 만들어라. 스테이지의 정보를 저장하는 리스트를 만들어라.
    public static EUserCreateMode eUserCreateMode;   // 유저를 생성 할 때 GPGS ID를 이용하여 생성하는지, GPGS ID를 이용하지 않고 생성하는지 구분

    static PlayerInformation()
    {
        userData = new UserData();
        inventory = new UserInventory();
        stageData = new StageData();
    }
}
