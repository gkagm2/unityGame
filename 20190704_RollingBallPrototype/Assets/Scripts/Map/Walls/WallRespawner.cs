using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRespawner : MonoBehaviour {

    public GameObject[] wall;

    [Header("Wall Respawn Time")]
    public float wallRespawnMaxTime = 5.0f;
    float wallRespawnTimer = 0f; // first wall distance
    
    public float wallSpeed = 10.0f;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        RespawnWall(); // 벽 생성


    }

    // 벽 생성
    public void RespawnWall()
    {

        wallRespawnTimer += Time.deltaTime;

        if(wallRespawnTimer >= wallRespawnMaxTime)
        {
            // TODO : 레벨 별로 벽 움직임 제어.
            Quaternion qRotation = Quaternion.Euler(90f, 0f, 0f);
            GameObject newWall= Instantiate(wall[0], transform.position, qRotation);
            if (newWall) // 생성 될 시
            {
                newWall.transform.Rotate(0, 0, Random.Range(0, 360), Space.World);
                newWall.GetComponent<Wall>().speed = wallSpeed;
            }

            wallRespawnTimer = 0;
        }

    }

    // 벽 패턴
    public void RespawnWallPattern()
    {

    }


    // 레벨 변경
    public void ChangeLevel()
    {
        switch (Level.currentLevel) // 변경 시 설정 값 변경
        {
            case LevelState.level1:
                wallRespawnTimer = 5.0f; // TODO : 변수로 받기.
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
}
