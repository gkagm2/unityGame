using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; // need

public class LookAtPointEditor : Editor { //Editor를 상속

    public override void OnInspectorGUI()
    {
        LookAtPoint _target = target as LookAtPoint;
        _target.lookAtPoint = EditorGUILayout.Vector3Field("Look At Point", _target.lookAtPoint);
        if (GUI.changed)
        {
            EditorUtility.SetDirty(_target);
        }
        //base.OnInspectorGUI();
    }

    private void OnSceneGUI()
    {   
        LookAtPoint _target = target as LookAtPoint;
        _target.lookAtPoint = Handles.PositionHandle(_target.lookAtPoint, Quaternion.identity);
        if (GUI.changed)
        {
            EditorUtility.SetDirty(_target);
        }
    }
}
