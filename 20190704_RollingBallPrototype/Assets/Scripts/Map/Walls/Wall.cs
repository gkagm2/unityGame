using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : ObjectController {
    
    // Update is called once per frame
    void Update () {
        if (BallGameManager.instance.isPlayerFail)
            return;

        Debug.DrawLine(transform.position, Vector3.zero, Color.red);

        DestroyWhenArriveDestination();
        Move();
	}
    
    // 움직임 변화.
    public override void SetMoveMent(LevelStatus currentLevel)
    {
        base.SetMoveMent(currentLevel);

        switch (currentLevel)
        {
            case LevelStatus.level1:
                rotationSpeedZ = 0;
                break;
            case LevelStatus.level2:
                rotationSpeedZ = 50f * Random.Range(-1, 2) * Time.deltaTime; // 1, 0, -1값으로 회전, 회전 방향 주기
                break;
            case LevelStatus.level3:
                rotationSpeedZ = 60f * Random.Range(-1, 2) * Time.deltaTime;
                break;
            case LevelStatus.level4:
                rotationSpeedZ = 70f * Random.Range(-1, 2) * Time.deltaTime;
                break;
            case LevelStatus.level5:
                rotationSpeedZ = 80f * Random.Range(-1, 2) * Time.deltaTime;
                break;
            case LevelStatus.level6:
                rotationSpeedZ = 90f * Random.Range(-1, 2) * Time.deltaTime;
                break;
            case LevelStatus.level7:
                rotationSpeedZ = 100f * Random.Range(-1, 2) * Time.deltaTime;
                break;
        }
    }

}
