using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    public bool gameOver = false;
    SceneMrg sceneManager;
    //PlayerInfo playerinfo;
    public EnemyRespawner enemyRespawner;
    public PlayerInfoScript playerInfo;
    


    // Use this for initialization
    void Start () {
        sceneManager = GetComponent<SceneMrg>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!gameOver)
        {
            playerInfo.score +=  Time.deltaTime * 10 + 0.05f;
        }
        GameKey();
    }
    //public void nemyESlowlyGenerate(float value = 0.3f)
    //{
    //    enemyRespawner.respawnTime += value;
    //}
    public void GameOver()
    {
        gameOver = true;
    }

    public void GameKey()
    {
        if (Input.GetKey(KeyCode.R))
        {
            Debug.Log("R 버튼 클릭함");
            sceneManager.GotoGameSceneBtn("Game");
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            sceneManager.GotoGameSceneBtn("Main");
        }

    }
}
