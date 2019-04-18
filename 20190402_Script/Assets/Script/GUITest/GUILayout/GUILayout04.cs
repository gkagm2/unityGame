using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUILayout04 : MonoBehaviour {

    //자동 레이아웃: 영역(Areas)
    /*
     * 영역은 자동 레이아웃 모드에서 사용 됨. GUILayout컨트롤을 포함하는 화면에서 그려질
     * 부분을 정의하는 것으로 고정 레이아웃의 그룹과 비슷하다. 자동 레이아웃의 성격 때문에,
     * 주로 영역(Areas)을 같이 사용하게 된다.
     * 자동 레이아웃 모드에서는 컨트롤이 그려지는 화면의 영역을 정의하지 않습니다. 자동 레이아웃
     * 모드에서는 자동으로 그려질 영역이 화면의 왼쪽 위 지점으로부터 배치됩니다. 물론 수동으로 위치할
     * 영역(Area)을 만들 수도 있다.
     */
    void OnGUI()
    {
        GUILayout.Button("I am not inside an Area");
        GUILayout.BeginArea(new Rect(Screen.width / 2, Screen.height / 2, 300, 300));
        GUILayout.Button("I am completely inside an Area");
        GUILayout.EndArea();
    }

}
