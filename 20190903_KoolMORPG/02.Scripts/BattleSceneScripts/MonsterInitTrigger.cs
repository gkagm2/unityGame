using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInitTrigger : MonoBehaviour
{
    public StageManager stageManager;

    private SphereCollider sphereCollider;

    private void Start()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))                              //플레이어와 충돌시
        {
            stageManager.MonsterEnable();                           //스테이지 매니저 클래스에서 몬스터 활성화 함수 실행
            sphereCollider.enabled = false;                         //콜라이더 비활성화
            if (stageManager.areaIdx < stageManager.area.Length)    //스테이지가 끝이 아닐 경우
            {
                stageManager.areaIdx++;                             //스테이지 매니저 클래스의 구역 번호 ++;
                stageManager.NextArea();                            //스테이지 매니저 클래스에서 다음 구역 초기화
            }
        }
       
    }
}
