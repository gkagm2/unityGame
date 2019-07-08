using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRespawner : MonoBehaviour {

    public GameObject[] Walls;

    public float wallRespawnTimer = 5.0f; // first wall distance
    public float []wallRespawnTime;


    float wallRespawnMaxTime;
    // Use this for initialization
    void Start() {
        wallRespawnMaxTime = wallRespawnTimer;


        // TODO : 여기서부터 한다.
        wallRespawnTime = new float[Level.levelCount];

    }

    // Update is called once per frame
    void Update() {
        RespawnWall(); // 벽 생성


    }

    // 벽 생성
    public void RespawnWall()
    {

        wallRespawnTimer -= Time.deltaTime;

        switch (Level.currentLevel)
        {
            case LevelState.level1:
                wallRespawnTimer = 5.0f;
                break;
            case LevelState.level2:
                break;
            case LevelState.level3:
                break;
            case LevelState.level4:
                break;
            case LevelState.level5:
                break;
            case LevelState.level6:
                break;
            case LevelState.level7:
                break;
        }
    }

    // 레벨 세팅
    public void LevelSetting()
    {

    }
}
