using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Main Camera에 붙이자.
public class LookAtPoint : MonoBehaviour {
    /// <summary>
    /// 월드 좌표계의 지점을 지속적으로 바라보게 함. 항상 오브젝트가 하나의 지점을 바라보는 간단한 코드
    /// </summary>
    public Vector3 lookAtPoint = Vector3.zero;

	// Update is called once per frame
	void Update () {
        //실행 버튼을 
        transform.LookAt(lookAtPoint);
	}
}
