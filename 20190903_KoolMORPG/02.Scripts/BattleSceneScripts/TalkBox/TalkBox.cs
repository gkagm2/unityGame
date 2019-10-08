/// DATE : 20190923
/// NAME : 장현명

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 대화 박스
/// </summary>
public class TalkBox : MonoBehaviour
{
    [Header("대화 내용")]
    public Text talkText;           // 대화 텍스트

    [Header("왼쪽 대화자")]
    public GameObject talkerLeftObj;   // 왼쪽 대화하는 캐릭터. (Obj : GameObject)
    public Image leftFaceImage;         // 왼쪽편 얼굴 이미지
    public Text leftTalkerNameText;     // 왼쪽편 이름

    [Header("오른쪽 대화자")]
    public GameObject talkerRightObj;  // 오른쪽 대화하는 캐릭터. (Obj : GameObject)
    public Image rightFaceImage;        // 오른쪽편 얼굴 아미지
    public Text rightTalkerNameText;    // 오른쪽편 이름

    /// <summary>
    /// 대화 할 때 대화창을 설정한다.
    /// </summary>
    /// <param name="speak">말하는 내용</param>
    /// <param name="name">말하는자의 이름</param>
    /// <param name="face">말하는자의 얼굴 사진</param>
    /// <param name="talker">말하는 위치</param>
    public void Speak(string speak, string name, Sprite face, ETalker talker)
    {
        switch (talker)
        {
            case ETalker.leftTalker:
                talkerLeftObj.SetActive(true);
                talkerRightObj.SetActive(false);

                leftFaceImage.sprite = face;
                leftTalkerNameText.text = name;
                break;
            case ETalker.rightTalker:
                talkerLeftObj.SetActive(false);
                talkerRightObj.SetActive(true);

                rightTalkerNameText.text = name;
                rightFaceImage.sprite = face;
                break;
        }
        talkText.text = speak;
    }
}
public enum ETalker // 왼쪽, 오른쪽의 대화하는 캐릭터 구분용도
{
    leftTalker,
    rightTalker
}