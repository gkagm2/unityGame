using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMoving : MonoBehaviour {
    GameManager gameManager;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameMrg").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameManager.IsGameOver())
        {
            if(transform.position.y > 800)
            {
                transform.Translate(0.0f, -10f, 0.0f);
            }
        }
	}
}
