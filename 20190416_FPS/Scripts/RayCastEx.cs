using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastEx : MonoBehaviour {

    void OnCreate() { }
    void OnUpdate() { }

    void OnDrawGizmos()
    {

        float maxDistance = 100;
        RaycastHit hit;
        // Physics.Raycast (레이저를 발사할 위치, 발사 방향, 충돌 결과, 최대 거리)
        bool isHit = Physics.Raycast(transform.position, transform.forward, out hit, maxDistance);
        Gizmos.color = Color.red;
        if (
            isHit)
        {
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
            //Debug.Log("충돌 결과 : " + hit.collider.gameObject.transform.position);
            //Debug.Log(hit.distance);
        }
        else
        {
            Gizmos.DrawRay(transform.position, transform.forward * maxDistance);
        }
    }
}