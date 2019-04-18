using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(RangeAttribute))]
public class RangeDrawer : PropertyDrawer {
    //주어진 영역 안에 속성 그리기
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        RangeAttribute range = attribute as RangeAttribute;

        if (property.propertyType == SerializedPropertyType.Float)
            EditorGUI.Slider(position, property, range.min, range.max, label.ToString());
        else if (property.propertyType == SerializedPropertyType.Integer)
            EditorGUI.IntSlider(position, property, (int)range.min, (int)range.max, label.ToString());
        else
            EditorGUI.LabelField(position, label.text, "Use Range with float or int.");
        //base.OnGUI(position, property, label);
    }

}
