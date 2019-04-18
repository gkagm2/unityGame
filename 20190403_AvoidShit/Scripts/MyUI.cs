using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUI : MonoBehaviour {
    public GUIStyle guiStyle; //GUI의 스타일을 인스펙터뷰에서 설정하기 위함
    
    private void OnGUI()
    {
        /// TODO : GetComponent를 이용하여 똥 피한 갯수를 EnemyControl 스크립트에서 얻어와야 함
        //GUI.Label(new Rect(10, 10, 30, 50), "피한 똥 : " + GetComponent<EnemyControl>().passedEnemyCount.ToString(), guiStyle);
    }
}
