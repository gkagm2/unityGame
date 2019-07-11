using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour {

    // respawn time
    public float respawnMaxTime;
    protected float respawnTimer = 0f; // first distance

    // 일정한 주기마다 오브젝트 생성
    public virtual void RespawnObjectAtRegularCycle(GameObject targetObj, float coolTime)
    {
        respawnTimer += Time.deltaTime;
        
        if (respawnTimer >= coolTime)
        {
            respawnTimer = 0;
        }
    }

    
    // 오브젝트 생성.
    public virtual void CreateObject()
    {
        
    }

    // 레벨에 따른 설정값 변경
    public virtual void ChangeLevel(LevelState currentLevel)
    {
        switch (currentLevel) // 변경 시 설정 값 변경
        {
            case LevelState.level1:
                respawnMaxTime = 5.0f; // 시간 변경 TODO : 변수로 받기.
                break;
            case LevelState.level2:
                respawnMaxTime = 3.0f; // 시간 변경 TODO : 변수로 받기.
                break;
            case LevelState.level3:
                respawnMaxTime = 2.0f; // 시간 변경 TODO : 변수로 받기.
                break;
            case LevelState.level4:
                respawnMaxTime = 1.8f;
                break;
            case LevelState.level5:
                respawnMaxTime = 1.6f;
                break;
            case LevelState.level6:
                respawnMaxTime = 1.4f;
                break;
            case LevelState.level7:
                respawnMaxTime = 1.2f;
                break;
        }
    }
}










