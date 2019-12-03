using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MORPG_TalkBoxController : MonoBehaviour
{
    public TalkBox talkbox;
    public MORPG_TalkManager talkManager;
    public bool isAction;
    public GameObject scanObject;
    public int talkIndex;

    // temp
    public uint talkerId;
    public bool isNpc;

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        MORPG_CharacterID objData = scanObject.GetComponent<MORPG_CharacterID>();
        if (objData)
        {
            talkerId = objData.id;
            Talk(objData.id, objData.isNpc);
        }

        talkbox.gameObject.SetActive(isAction);
        talkbox.talkerImage.sprite = objData.characterSprite;
    }

    public void Talk(uint id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            talkbox.talkerImage.sprite = null;
            talkbox.gameObject.SetActive(false);
            return;
        }

        if (isNpc)
        {
            talkbox.talkText.text = talkData;
        }
        else
        {
            talkbox.talkText.text = talkData;
        }
        
        isAction = true;
        ++talkIndex;
    }
}