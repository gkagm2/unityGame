/// DATE : 20190923
/// NAME : 장현명

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// 대화 박스
/// </summary>
public class TalkBox : MonoBehaviour
{
    public MORPG_TalkBoxController talkboxController;
    public Text talkText;
    public Image talkerImage;
    public Text talkerNameText;

    public void NextTalk()
    {
        talkboxController.Talk(talkboxController.talkerId, talkboxController.isNpc);
    }
}   