using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MORPG_TalkManager : MonoBehaviour
{
    Dictionary<uint, string[]> talkData; // key : character id, value : talk message
    Dictionary<uint, Sprite> portraitData; // 인물 사진

    public Sprite[] portraitArray;

    private void Start()
    {
        talkData = new Dictionary<uint, string[]>();
        GenerateData();
    }
    
    public string GetTalk(uint id, int talkIndex)
    {
        if(talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
        {
            return talkData[id][talkIndex];
        }
    }

    private void GenerateData()
    {
        // TODO (장현명) : Save, Load
        talkData.Add(100, new string[] { "반갑군", "어서오시게" });
        talkData.Add(200, new string[] { "안녕하시게나", "좀비를 10마리만 잡아다오." });
    }

    public Sprite GetPortrait(uint id, int portraitIndex)
    {
        return portraitData[id + (uint)portraitIndex];
    }
}
