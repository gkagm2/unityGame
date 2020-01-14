using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MailBoxPopupManager : MonoBehaviour
{
    public GameObject content;      // 아이템을 생성할 부모 오브젝트

    /// <summary>
    /// 보상을 받는다.
    /// 보상의 타입에 따라 content안에 생성되는 카드가 다르게 생성됨
    /// </summary>
    /// <param name=""></param>
    public void ReceiveMail(ERewardType rewardType)
    {
        // TODO (장현명) : 보상의 타입에 따라 아이템 카드를 생성 후 content 오브젝트의 자식으로 넣기
        switch (rewardType)
        {
            case ERewardType.equipmentItem:
                break;
            case ERewardType.gold:
                break;
            case ERewardType.crystal:
                break;
            case ERewardType.stat:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// TODO (장현명) : 완성 후에 지운다.
    /// </summary>
    /// <param name="type"></param>
    public void TestReceive(int type)
    {
        if(type == 1)
        {
            PlayerInformation.userData.crystal += 50;
        }
        else
        {
            PlayerInformation.userData.crystal += 20;
            PlayerInformation.userData.gold += 1000;
        }
        StartCoroutine(ITestReceive());
    }
    public IEnumerator ITestReceive()
    {
        if(NetworkManager.instance != null)
        {
            yield return NetworkManager.instance.ISaveCharacterDataToServer();
        }
        TopPopupManager.instance.TopPopupsUpdate();
    }
}
