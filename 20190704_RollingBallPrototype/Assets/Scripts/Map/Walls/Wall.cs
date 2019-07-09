using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    public float speed = 10.0f; // wall speed
    public float rotationSpeed = 0f;
    float axisZ = 0f;
    public float fallingAcceleration = 0.05f; // 가속도

    // Update is called once per frame
    void Update () {
        Debug.DrawLine(transform.position, Vector3.zero, Color.red);
        
        // 끝까지 가면 없애기
        if (transform.position.z < -10)
        {
            Destroy(gameObject);
        }

        Move();
	}


    // 벽 움직임
    public void Move()
    {
        speed += fallingAcceleration; // 가속도 붙음

        transform.Translate(0, 0, -(speed * Time.deltaTime), Space.World);
        transform.Rotate(0, 0, axisZ, Space.World);
    }
    
    // 움직임 변화.
    public void SetWallMoveMent(LevelState levelState)
    {
        switch (levelState)
        {
            case LevelState.level1:
                axisZ = 0f;
                speed = 20.0f;
                break;
            case LevelState.level2:
                rotationSpeed = 50f;
                axisZ = rotationSpeed  * Random.Range(-1, 2) * Time.deltaTime; // 1, 0, -1값으로 회전, 회전 방향 주기
                speed = 25.0f;
                break;
            case LevelState.level3:
                rotationSpeed = 60f;
                axisZ = rotationSpeed * Random.Range(-1, 2) * Time.deltaTime;
                speed = 30.0f;
                break;
            case LevelState.level4:
                rotationSpeed = 70f;
                axisZ = rotationSpeed * Random.Range(-1, 2) * Time.deltaTime;
                speed = 35.0f;
                break;
            case LevelState.level5:
                rotationSpeed = 80f;
                axisZ = rotationSpeed * Random.Range(-1, 2) * Time.deltaTime;
                speed = 40.0f;
                break;
            case LevelState.level6:
                rotationSpeed = 90f;
                axisZ = rotationSpeed * Random.Range(-1, 2) * Time.deltaTime;
                speed = 45.0f;
                break;
            case LevelState.level7:
                rotationSpeed = 100f;
                axisZ = rotationSpeed * Random.Range(-1, 2) * Time.deltaTime;
                speed = 50.0f;
                break;
        }
    }

}
