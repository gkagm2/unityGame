using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape)) // 종료 키 누를 경우
        {
            Application.Quit(); // 게임 종료
        }
    }
}
