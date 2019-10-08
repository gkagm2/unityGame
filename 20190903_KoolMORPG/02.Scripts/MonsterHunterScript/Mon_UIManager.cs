using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mon_UIManager : MonoBehaviour
{
    public GameObject playerObject;
    private Mon_CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = playerObject.GetComponent<Mon_CharacterController>();
    }

    private void Update()
    {
        // player controller
        if (Input.GetButtonDown("Mon_Skill1"))
        {
            Debug.Log("mon Mon_Skill1");
            controller.AttackAimStandby();
        }
        if (Input.GetButtonDown("Mon_Skill2"))
        {
            Debug.Log("mon Mon_Skill2");
            controller.AttackAimShoot();
        }
        if (Input.GetButtonDown("Mon_DefaultAttack"))
        {
            Debug.Log("mon Mon_DefaultAttack");
            controller.AttackNormal();
        }
        if (Input.GetButtonDown("Mon_Dash"))
        {
            Debug.Log("mon Mon_Dash");
            controller.Dash();
        }
    }
}
