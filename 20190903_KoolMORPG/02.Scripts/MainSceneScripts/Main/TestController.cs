using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    /// <summary>
    /// 재화 추가하기 버튼 클릭
    /// </summary>
    public void OnClick_AddGoods()
    {
        PlayerInformation.userData.gold += 1000;
        PlayerInformation.userData.crystal += 500;
        PlayerInformation.userData.Stamina += 10;
        // TODO (장현명) : TOP
        TopPopupManager.instance.TopPopupsUpdate();
    }
}