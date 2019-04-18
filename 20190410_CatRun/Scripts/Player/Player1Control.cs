using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Control : MonoBehaviour
{
    PlayerControl playerControl;

    void Start()
    {
        playerControl = GetComponent<PlayerControl>();
    }

    void Update()
    {
        playerControl.Jump(KeyCode.W);
        playerControl.PickShield(KeyCode.A);
    }




}
