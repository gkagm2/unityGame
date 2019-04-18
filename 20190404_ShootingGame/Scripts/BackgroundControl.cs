using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundControl : MonoBehaviour
{
    bool state;
    public float speed = 0.6f;
    GameManager gameManager;
    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameMrg").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //게임이 끝나지 않았으면
        if (!gameManager.IsGameOver())
        {
            float moveAmt = speed * Time.deltaTime;

            transform.Translate(Vector3.down * moveAmt, Space.World); //맵이 밑으로 내려옴

            if (transform.position.y <= -13.571)
            {
                transform.position = new Vector3(0.0f, 40.173f, 8.0f);
            }
        }
        
    }
}
