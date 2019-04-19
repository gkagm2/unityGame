using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScrolling : MonoBehaviour {

    public float scrollSpeed = 0.5f;
    GameManager gameManager;
    Renderer rend;

	// Use this for initialization
	void Start () {
        //mesh render를 rend에 넣음.
        rend = gameObject.GetComponent<Renderer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        //게임 오버가 아니면
        if (!gameManager.gameOver)
        {
            float offset = Time.time * scrollSpeed;

            rend.material.SetTextureOffset("_MainTex", new Vector2(0, -offset)); 
        }
	}
}
