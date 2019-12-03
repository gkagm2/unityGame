using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MORPG_NpcController : MORPG_Interactable
{
    private MORPG_TalkBoxController talkBoxController;
    private void Start()
    {

        talkBoxController = GameObject.Find("PlayerUI").GetComponent<MORPG_TalkBoxController>();
        if (talkBoxController)
        {
        }
    }

    public override void Interact()
    {
        base.Interact();
        Debug.Log("Interacted with " + gameObject.name + " override yeah!");
        //talkBoxController.talkbox.gameObject.SetActive(true);
        talkBoxController.Action(gameObject);
    }
}
