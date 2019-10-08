using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 로딩화면 클래스
/// </summary>
public class LoadingPanel : MonoBehaviour
{
    [HideInInspector] public Image leftDiskImage;   // 왼쪽 디스크 이미지
    [HideInInspector] public Image rightDiskImage;  // 오른쪽 디스크 이미지
    [HideInInspector] public Text tipText;          // 팁 텍스트

    [Header("로딩 타입")] [Tooltip("coolTime : coolTime을 줘서 끈다, steady : 항상 켜져있고 임의로 꺼야 한다.")]
    public ELoadingType loadingType;

    [Tooltip("로딩 타입이 coolTime일 때 시간값에 따라 작동함.(sec)")]
    public float coolTime = 1.0f;
    public enum ELoadingType
    {
        coolTime,   // 쿨타임을 줘서 멈추는 방법.
        steady      // 항상 켜져있어서 임의로 꺼야 한다.
    }                     // 로딩 타입

    [System.Serializable]
    public class Tip
    {
        public string tipText;
    }

    [Header("팁 내용을 입력하면 랜덤으로 선택되서 나온다.")] [Tooltip("tip을 입력하시오")]
    public Tip[] tip;

    private readonly float rotationSpeed = 100f;    // 디스크의 회전 속도
    void Start()
    {
        tipText.text = tip[UnityEngine.Random.Range(0, tip.Length)].tipText;

        switch (loadingType)
        {
            case ELoadingType.coolTime:
                StartCoroutine(ILoadingCoolTime());
                break;
            case ELoadingType.steady:
                break;
            default:
                Debug.Assert(false);
                break;
        }
    }

    /// <summary>
    /// 굴라가기 업데이트
    /// </summary>
    void Update()
    {
        // 왼쪽, 오른쪽 디스크 이미지들을 움직이게 한다.
        leftDiskImage.transform.Rotate(0, 0, -Time.deltaTime * rotationSpeed);
        leftDiskImage.transform.Translate(Time.deltaTime * 200f, 0, 0, Space.World);
        rightDiskImage.transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
        rightDiskImage.transform.Translate(-Time.deltaTime * 200f, 0, 0, Space.World);
    }

    /// <summary>
    /// 쿨타임이 끝나면 스크립트를 종료한다.
    /// </summary>
    /// <returns>쿨타임</returns>
    public IEnumerator ILoadingCoolTime()
    {
        yield return new WaitForSeconds(coolTime);
        gameObject.SetActive(false);
    }
}
