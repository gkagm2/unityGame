using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class MORPG_PlayerQuest : MonoBehaviour
{
    List<tagQuestInfo> questList;
    tagQuestInfo questInfo;

    string directoryName;
    string fileName;
    // Start is called before the first frame update
    void Start()
    {
        // TODO (장현명) : 퀘스트 구상 후 리펙토링 및 데이터 파일 형태로 저장.
        questList = new List<tagQuestInfo>();

        questInfo.SetQuest("늑대 퇴치", "늑대 10마리를 퇴치해주세요", false, 0);
        questList.Add(questInfo);
        questInfo.SetQuest("돼지 퇴치", "돼지 20마리를 퇴치해주세요", false, 0);
        questList.Add(questInfo);
        directoryName = "testDic";
        fileName = "info.i";


        SaveLoadManager.instance.CreateDirectory(directoryName);
        //SaveLoadManager.instance.Save(directoryName, fileName);
        SaveLoadManager.instance.Load(directoryName, fileName);
    }
}