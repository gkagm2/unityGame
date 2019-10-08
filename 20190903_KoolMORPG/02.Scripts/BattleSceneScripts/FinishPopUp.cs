using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPopUp : MonoBehaviour
{
    private Animator am;

    private void Awake()
    {
        am = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        am.SetTrigger("OnEnable");
    }


}
