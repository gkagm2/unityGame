using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MORPG_PlayerInformation : MonoBehaviour
{
    [SerializeField]
    private UserData userData;

    private void Start()
    {
        userData = PlayerInformation.userData.GetInGameCharacterData();
    }
}
