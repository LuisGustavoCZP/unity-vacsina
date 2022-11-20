using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(GAgentVisual))]
[CanEditMultipleObjects]
public class GAgentVisualEditor : Editor 
{
    void OnEnable()
    {

    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();
        GAgentVisual agent = (GAgentVisual) target;
        var gagent = agent.gameObject.GetComponent<GAgent>();

        GUILayout.Label("Name: " + agent.name);
        GUILayout.Label("Current Action: " + gagent.currentAction?.action);
        GUILayout.Label("Actions: ");
        foreach (GAction a in gagent.actions)
        {
            if (a == null) continue;
            string pre = "";
            string eff = "";

            foreach (var p in a.conditions)
                pre += p.key + ", ";
            foreach (var e in a.effects)
                eff += e.key + ", ";

            GUILayout.Label("====  " + a.actionName + "(" + pre + ")(" + eff + ")");
        }
        GUILayout.Label("Goals: ");
        foreach (var pair in gagent.goalSet)
        {
            if (pair.Key == null) continue;
            GUILayout.Label($"{pair.Key.name}({pair.Value}): ");
            foreach (GState sg in pair.Key.states)
                GUILayout.Label("=====  " + sg.key);
        }
        GUILayout.Label("Beliefs: ");
        foreach (KeyValuePair<string, int> sg in gagent.beliefs.GetStates())
        {
                GUILayout.Label("=====  " + sg.Key);
        }

        GUILayout.Label("Inventory: ");
        foreach (var r in gagent.inventory.items)
        {
            GUILayout.Label("====  " + r.gameObject.tag);
        }


        serializedObject.ApplyModifiedProperties();
    }
}