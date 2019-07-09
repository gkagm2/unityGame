using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRespawner : MonoBehaviour {

    public GameObject[] wall;

    [Header("Wall Respawn Time")]
    public float wallRespawnMaxTime = 5.0f;
    float wallRespawnTimer = 0f; // first wall distance


    public GameObject wallRespawnEffect; // 벽이 생성될 때 나오는 effect


    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        RespawnWall(wallRespawnMaxTime); // 벽 생성


    }

    // 벽 생성
    public void RespawnWall(float coolTime)
    {
        wallRespawnTimer += Time.deltaTime;

        if(wallRespawnTimer >= coolTime)
        {
            
            // TODO : 레벨 별로 벽 움직임 제어.
            Quaternion qRotation = Quaternion.Euler(90f, 0f, 0f);
            GameObject newWall= Instantiate(wall[Random.Range(0,4)], transform.position, qRotation);
            if (newWall) // 생성 될 시
            {
                ChangeLevel(Level.currentLevel);
                newWall.GetComponent<Wall>().SetWallMoveMent(Level.currentLevel); // 현재 레벨 세팅해준다.
                newWall.transform.Rotate(0, 0, Random.Range(0, 360), Space.World); // 생성 할 때의 각도 설정
            }
            wallRespawnTimer = 0;
        }

    }

    // 벽 패턴
    public void RespawnWallPattern()
    {

    }


    // 레벨에 따른 설정값 변경
    public void ChangeLevel(LevelState currentLevel)
    {
        switch (currentLevel) // 변경 시 설정 값 변경
        {
            case LevelState.level1:
                wallRespawnMaxTime = 5.0f; // 시간 변경 TODO : 변수로 받기.
                break;
            case LevelState.level2:
                wallRespawnMaxTime = 3.0f; // 시간 변경 TODO : 변수로 받기.
                break;
            case LevelState.level3:
                wallRespawnMaxTime = 2.0f; // 시간 변경 TODO : 변수로 받기.
                break;
            case LevelState.level4:
                wallRespawnMaxTime = 1.8f;
                break;
            case LevelState.level5:
                wallRespawnMaxTime = 1.6f;
                break;
            case LevelState.level6:
                wallRespawnMaxTime = 1.4f;
                break;
            case LevelState.level7:
                wallRespawnMaxTime = 1.2f;
                break;
        }
    }
}
