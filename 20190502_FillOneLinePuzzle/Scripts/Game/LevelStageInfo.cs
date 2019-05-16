using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
// 레벨과 스테이지의 정보를 담는 클래스
public class LevelStageInfo {
    public GameObject obj;
    public short level = 1; // 레벨
    public short stage = 1;  // 스테이지
    public bool isSuccess = false; // 깼는지 안깼는지
}