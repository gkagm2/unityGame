using UnityEngine;
using UnityEditor;
/*
 * 속성 정의(PropertyDrawer)를 사용해서 스크립트 멤버 변수의 컨트롤을 수정할 수 있습니다.속성 정의의 다른 용도로 인스펙터 뷰에 표시되는 스크립트에서
 * 노출된 변수들의 표현 방식을 변경할 수 있습니다. 또한, 정수 변수를 특정 범위로 제한하고자 한다면 RangeAttribute라는 속성을 사용할 수 있습니다.
 */ 
[CustomPropertyDrawer(typeof(Ingredient))]
public class IngredientDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        Rect amountRect = new Rect(position.x, position.y, 30, position.height);
        Rect unitRect = new Rect(position.x + 35, position.y, 50, position.height);
        Rect nameRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);

        EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("amount"),GUIContent.none);
        EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("unit"), GUIContent.none);
        EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
        

        //base.OnGUI(position, property, label);
    }
}
