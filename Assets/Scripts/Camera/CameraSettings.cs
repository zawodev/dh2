using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class CameraSettings{

    public Transform target;
    public float moveSpeed = 6;
    public float magnifySpeed = 6;
    public float size = 5;

    //[Tooltip("Do animacji")]
    [HideInInspector]
    public Vector3 dynamicOffset; //useless i think
    public Vector3 staticOffset = new Vector3(0f, 1f, 0f);

    public bool bounds = true;

    public Vector3 minCameraPos = new Vector3(-230f, -5f, -10f);
    public Vector3 maxCameraPos = new Vector3(230f, 5f, -10f);

}
/*
[CustomPropertyDrawer(typeof(CameraSettings))]
public class CameraSettingsEditor : PropertyDrawer {
    public override void OnGUI(Rect rect, SerializedProperty prop, GUIContent label) {

        EditorGUI.BeginProperty(rect, label, prop);
        bool bounds = prop.FindPropertyRelative("bounds").boolValue;

        if (bounds) {

        }

        SerializedProperty sp = prop.FindPropertyRelative("minCameraPos");
        SerializedProperty sp2 = prop.FindPropertyRelative("maxCameraPos");
        EditorGUI.indentLevel++;

        sp.vector3Value = EditorGUI.Vector3Field(new Rect(rect.x, rect.y + 16, rect.width, 16), "minCameraPos", sp.vector3Value);
        sp2.vector3Value = EditorGUI.Vector3Field(new Rect(rect.x, rect.y + 16, rect.width, 16), "maxCameraPos", sp2.vector3Value);
        EditorGUI.indentLevel--;
    }
}
*/

