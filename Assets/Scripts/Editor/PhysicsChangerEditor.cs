using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PhysicsChanger))]
public class PhysicsChangerEditor : Editor
{


    public override void OnInspectorGUI()
    {

        var physicsChanger = ((PhysicsChanger)target);
        physicsChanger.balanceVelocities();
        base.OnInspectorGUI();
    }

}