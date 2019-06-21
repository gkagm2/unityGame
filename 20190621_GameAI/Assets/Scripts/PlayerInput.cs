using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    CharacterScript player;
	// Use this for initialization
	void Start () {
        player = GetComponent<CharacterScript>();
	}
	
	// Update is called once per frame
	void Update () {
        InputOperationKeys();
	}

    public void InputOperationKeys()
    {

        // Movement
        float dirZ = Input.GetAxis("Horizontal");
        float dirY = Input.GetAxis("Vertical");
        player.Move2(dirZ, dirY);


        // Gun fire
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.Fire();
        }

    }
}
