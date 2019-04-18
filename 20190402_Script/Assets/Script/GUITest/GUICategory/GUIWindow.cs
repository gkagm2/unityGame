using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIWindow : MonoBehaviour {

    private Rect windowRect = new Rect(20, 20, 100, 150);

    private void OnGUI()
    {
        //윈도우는 컴트롤 드래그할 수 있는 컨트롤의 집합이다. 이러한 컨트롤들은 윈도우 컨트롤 안에서 이벤트를 받아들일 수도 있고 포커스를 잃을 수도 있다.
        // 이 때문에 일반적으로 구현하는 것과는 약간 다르게 구현한다.
        //윈도우 컨트롤은 컨트롤이 제대로 동작하기 위해  추가로 함수가 필요한 유일한 UnitytGUI컨트롤이다.
        //윈도우 컨트롤은 제어에 필요한 ID번호와 함수의 이름을 제공해야 한다.
        //윈도우 함수 내에서 실제 동작이나 포함된 컨트롤들을 만들 수 있다.
        windowRect = GUI.Window(0, windowRect, WindowFunction, "MyWindow");
    }
    void WindowFunction(int windowID)
    {
        //윈도우 안에 들어갈 GUI 삽입 부분
    }

}
