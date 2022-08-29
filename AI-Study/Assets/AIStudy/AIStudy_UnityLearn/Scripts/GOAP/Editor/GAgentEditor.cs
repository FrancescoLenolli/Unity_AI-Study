using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using GOAP;

[CustomEditor(typeof(AgentVisual))]
[CanEditMultipleObjects]
public class AgentVisualEditor : Editor 
{


    void OnEnable()
    {

    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();
        AgentVisual agent = (AgentVisual) target;
        GUILayout.Label("Name: " + agent.name);
        GUILayout.Label("Current Action: " + agent.gameObject.GetComponent<GOAP.Agent>().currentAction);
        GUILayout.Label("Actions: ");
        foreach (Action action in agent.gameObject.GetComponent<GOAP.Agent>().actions)
        {
            string pre = "";
            string eff = "";

            foreach (KeyValuePair<string, int> p in action.Preconditions)
                pre += p.Key + ", ";
            foreach (KeyValuePair<string, int> e in action.Effects)
                eff += e.Key + ", ";

            GUILayout.Label("====  " + action.Name + "(" + pre + ")(" + eff + ")");
        }
        GUILayout.Label("Goals: ");
        foreach (KeyValuePair<SubGoal, int> g in agent.gameObject.GetComponent<GOAP.Agent>().goals)
        {
            GUILayout.Label("---: ");
            foreach (KeyValuePair<string, int> sg in g.Key.goals)
                GUILayout.Label("=====  " + sg.Key);
        }
        serializedObject.ApplyModifiedProperties();
    }
}