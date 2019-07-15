using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour {

    // respawn time
    public float respawnMaxTime =3f;
    protected float respawnTimer = 0f; // first distance

    // 일정한 주기마다 오브젝트 생성
    public virtual void RespawnObjectAtRegularCycle(float coolTime)
    {
    }

    
    // 오브젝트 생성.
    public virtual void CreateObject()
    {
    }

    // 레벨에 따른 설정값 변경
    public virtual void ChangeLevel(LevelStatus currentLevel)
    {
        ChangeRespawnSpeed(currentLevel); // 레벨에 따른 생성 속도 변경
    }

    // 레벨에 따른 생성 속도 변경
    protected void ChangeRespawnSpeed(LevelStatus currentLevel)
    {
        switch (currentLevel) // 변경 시 설정 값 변경
        {
            case LevelStatus.level1:
                respawnMaxTime = 5.0f; // 시간 변경 TODO : 변수로 받기.
                break;
            case LevelStatus.level2:
                respawnMaxTime = 3.0f; // 시간 변경 TODO : 변수로 받기.
                break;
            case LevelStatus.level3:
                respawnMaxTime = 2.0f; // 시간 변경 TODO : 변수로 받기.
                break;
            case LevelStatus.level4:
                respawnMaxTime = 1.8f;
                break;
            case LevelStatus.level5:
                respawnMaxTime = 1.6f;
                break;
            case LevelStatus.level6:
                respawnMaxTime = 1.4f;
                break;
            case LevelStatus.level7:
                respawnMaxTime = 1.2f;
                break;
        }
    }

}









