using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {
    public float speed = 10.0f; // object speed
    public float fallingAcceleration = 0.05f; // 가속도
    public float rotationSpeedZ = 0f;

    // 끝까지 가면 없애기
    public void DestoryWhenArriveDestination()
    {
        if (transform.position.z < -10)
            Destroy(gameObject);
    }

    // 움직임
    public virtual void Move()
    {
        Fall(Level.currentLevel);
        speed += fallingAcceleration; // 가속도 붙음
        transform.Translate(0, 0, -(speed * Time.deltaTime), Space.World);
        transform.Rotate(0, 0, rotationSpeedZ, Space.World);
    }

    // 떨어지다
    public void Fall(LevelState currentLevel)
    {
        switch (currentLevel)
        {
            case LevelState.level1:
                speed = 20.0f;
                break;
            case LevelState.level2:
                speed = 25.0f;
                break;
            case LevelState.level3:
                speed = 30.0f;
                break;
            case LevelState.level4:
                speed = 35.0f;
                break;
            case LevelState.level5:
                speed = 40.0f;
                break;
            case LevelState.level6:
                speed = 45.0f;
                break;
            case LevelState.level7:
                speed = 50.0f;
                break;
        }
    }

    // 움직임 변화.
    public virtual void SetMoveMent(LevelState currrentLevel)
    {

    }
}

