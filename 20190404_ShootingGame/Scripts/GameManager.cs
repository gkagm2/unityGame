using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UGUI사용하기 위해서 
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    bool gameOver = false;
    int score = 0;

    [Header("Level")]
    public int levelUpScore = 100; // 이 점수가 되면 레벨 업 됨.
    public int level = 1; //레벨

    [Header("UI")]
    public Text scoreText;
    public Text InfoText;
    public Text gameOverText;
    public Text playerHpText;
    public Text levelText;
    void Start()
    {
        gameOverText.text = "";
    }
    // Update is called once per frame
    void Update() {

        //UI
        levelText.text = "Level : " + (level);

        //게임이 오버 되면.
        if (gameOver)
        {
            //화면에 GameOver Text를 출력
            gameOverText.text = "GameOver \n" + EvaluateScore() + " Score \nrestart key : 'R'";

            //오버 된 상태에서 ESC Key를 누르면 
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                print("게임 종료");
                Application.Quit();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                //This is Old version
                /*  Application.LoadLevel(0);  */

                //NewVersion
                SceneManager.LoadScene("GamePlay");
            }


        }
        else
        {
            //레벨 보여주기
            if (score > levelUpScore * level)
            {
                ++level;
                print("levelup" + level);
            }
        }
	}
    public void PlusScore(int point)
    {

        score += point;
        scoreText.text = "Score : " + score.ToString();
    }

    public bool IsGameOver()
    {
        return gameOver;
    }
    public bool GameOver
    {
        get { return gameOver; }
        set { gameOver = value; }
    }
    string EvaluateScore()
    {
        string text;
        if (score > 100)
            text = "Perfect";
        else if (score > 60)
            text = "Good";
        else if (score > 30)
            text = "Bad";
        else
            text = "Worst";
        return text;
    }

}
