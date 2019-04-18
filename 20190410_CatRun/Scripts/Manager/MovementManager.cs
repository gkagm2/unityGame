using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour {
    GameManager gameManager;
    // enemy control
    public float enemySpeed = 0.5f;
    public float itemSpeed = 0.5f;

    // ground control
    public float groundSpeed = 0.5f; // 땅 속도
    Renderer rend;                   // Mesh Renderer

    // background control
    public float backgroundSpeed = 0.5f;
    Renderer backgroundRand;

	// Use this for initialization
	void Start () {
        gameManager = GetComponent<GameManager>();

        // ground setting
        rend = GameObject.Find("Ground").GetComponent<Renderer>(); //Mesh Renderer를 가져옴

        // background setting
        backgroundRand = GameObject.Find("Background").GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!gameManager.gameOver)
        {
            GroundMovementControl();         // ground movement control
            BackgroundMovementControl();     // backgound movement control
        }
        else
        {

        }
    }

    // ground movement control
    void GroundMovementControl()
    {
        float offset = Time.time * groundSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(-offset, 0));
    }
    // enemy movement control
    // background movement control
    void BackgroundMovementControl()
    {
        float offset = Time.time * backgroundSpeed;
        backgroundRand.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
