using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SimulationController))]
public class SimulationControllerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        var simCon = ((SimulationController)target);

        if (GUILayout.Button("Spawn Bodies"))
        {
            simCon.GenerateField();
        }

        base.OnInspectorGUI();
    }

}
