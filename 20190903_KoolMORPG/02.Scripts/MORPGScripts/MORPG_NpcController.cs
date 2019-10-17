using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MORPG_NpcController : MORPG_Interactable
{
    public override void Interact()
    {
        base.Interact();
        Debug.Log("Interacted with " + gameObject.name + " override yeah!");
    }
}
