using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    // player state
    public enum PlayerState
    {
        idle,
        left,
        right
    }
    public PlayerState playerState;

    // parameters
    public float speed = 0;



	// Use this for initialization
	void Start () {
        playerState = PlayerState.idle;
	}
	
	// Update is called once per frame
	void Update () {

        switch (playerState)
        {
            case PlayerState.idle:
                Debug.Log("Idle!");
                speed = 0;
                break;

            case PlayerState.left:
                Debug.Log("left!");
                transform.Translate(speed * Time.deltaTime, 0f, 0f);
                break;

            case PlayerState.right:
                Debug.Log("right!");
                transform.Translate(speed * Time.deltaTime, 0f, 0f);
                break;

            default:
                break;
        }
    }

    public void LeftClick()
    {
        Debug.Log("left button clicked!");
        speed = -10;
        playerState = PlayerState.left;
    }
    public void RightClick()
    {
        Debug.Log("right button clicked!");
        speed = 10;
        playerState = PlayerState.right;
    }
    public void IdleState()
    {
        playerState = PlayerState.idle;
    }
}
