using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
[System.Serializable]
// 레벨과 스테이지의 정보를 담는 클래스
public class LevelStageInfo {
    public GameObject obj;
    public short level; // 레벨
    public short stage;  // 스테이지
=======
// 레벨과 스테이지의 정보를 담는 클래스
public class LevelStageInfo {
    public GameObject obj;
    public int level; // 레벨
    public int stage; // 스테이지
>>>>>>> f9f39abfc1c605ebaec945526c7b4623d0bcee21
    public bool isSuccess; // 깼는지 안깼는지
}