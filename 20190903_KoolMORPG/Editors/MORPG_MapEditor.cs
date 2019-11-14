using UnityEngine;
using UnityEditor;

using System.Collections.Generic;

public class MORPG_MapEditor : EditorWindow
{
    // 적 생성구역 
    public int enemyRespawnAreaCount;
    public GameObject enemyRespawnArea;
    private string myString;

    // 맵 사물
    [SerializeField]
    public List<Object> obstacleObjList;
    private bool groupEnabled;
    private bool myBool = true;
    private float myFloat = 1.23f;
    private int obstacleCount = 0;


    [MenuItem("MORPG/맵 에디터")]
    static void ShowWindow()
    {
        EditorWindow.GetWindow<MORPG_MapEditor>("맵 에디터");
    }

    private void OnGUI()
    {
        // 적 생성구역
        EditorGUILayout.LabelField("적 생성구역 만들기", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("개수", myString);
        enemyRespawnAreaCount = int.Parse(myString);

        enemyRespawnArea = EditorGUILayout.ObjectField(enemyRespawnArea, typeof(GameObject), true) as GameObject;

        if (GUILayout.Button("생성구역 만들기")) // 적 생성존
        {
            CreateObject(enemyRespawnArea, enemyRespawnAreaCount);
        }

        // 맵 사물
        groupEnabled = EditorGUILayout.BeginToggleGroup("오브젝트 선택", groupEnabled);
        obstacleCount = int.Parse(EditorGUILayout.TextField("사물 오브젝트 생성 개수", myString));

        if (GUILayout.Button("사물 생성"))
        {
            for(int i =0; i < obstacleCount; ++i)
            {
                Object obj = new Object();
                obstacleObjList.Add(obj);
            }

            for (int i = 0; i < obstacleObjList.Count; ++i)
            {
                GameObject obj = EditorGUILayout.ObjectField(obstacleObjList[i], typeof(GameObject), true) as GameObject;
            }
        }
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("FuckSlider", myFloat, -3, 3);

        EditorGUILayout.EndToggleGroup();
        
        //if (GUILayout.Button("적용하기"))
        //{
        //    for (int i = 0; i < Selection.gameObjects.Length; ++i)
        //    {
        //        Debug.Log("총 선택된 개수 : " + Selection.gameObjects.Length + "선택된 GameObject name : " + Selection.gameObjects[i].name);
        //    }
        //}
    }

    private static void CreateObject(GameObject enemyRespawnArea, int maxCount)
    {
        for (int i = 0; i < maxCount; ++i)
        {
            Instantiate(enemyRespawnArea, new Vector3(0f, 0f, 0f), Quaternion.identity);
        }
    }
    
}
