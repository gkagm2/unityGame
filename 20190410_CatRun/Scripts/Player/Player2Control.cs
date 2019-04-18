using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Control : MonoBehaviour
{
    PlayerControl playerControl;

    void Start()
    {
        playerControl = GetComponent<PlayerControl>();
    }

    void Update()
    {
        playerControl.Jump(KeyCode.UpArrow);
        playerControl.PickShield(KeyCode.LeftArrow);

    }
}
