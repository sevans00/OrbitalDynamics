using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NBodyObject))]
public class NBodyObjectEditor : Editor {
    
    public override void OnInspectorGUI()
    {
        var nBodyObject = ((NBodyObject)target);
        nBodyObject.UpdateVolume();

        Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
        EditorGUI.FloatField(rect, nBodyObject.radius, GUIStyle.none);
        EditorGUILayout.Space();

        base.OnInspectorGUI();
    }
    
}
