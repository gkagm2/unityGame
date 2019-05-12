using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
// 레벨과 스테이지의 정보를 담는 클래스
public class LevelStageInfo {
    public GameObject obj;
    public short level; // 레벨
    public short stage;  // 스테이지
    public bool isSuccess; // 깼는지 안깼는지
}