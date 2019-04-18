using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    GameManager gameManager;

    [Header("UI")]
    public Text lifeText;
    public Text gameOverText;
    public Text scoreText;



	// Use this for initialization
	void Start () {
        gameManager = GetComponent<GameManager>();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (!gameManager.gameOver)
        {
            lifeText.text = "기회 : " + (gameManager.playerInfo.life + 1).ToString();
            scoreText.text = "점수 : " + ((int)gameManager.playerInfo.score).ToString();
        }
        else
        {
            lifeText.text = "기회 :" + " 끝";
            gameOverText.text = "Game Over\n\n 다시시작  : R\n\n뒤로가기: Esc";
            scoreText.text = "점수 : " + ((int)gameManager.playerInfo.score).ToString();



        }
    }
}