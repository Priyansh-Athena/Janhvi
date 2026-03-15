using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CollisionEvents))]
public class CollisionEventsEditor : Editor
{
    bool showMenu = false;

    public override void OnInspectorGUI()
    {
        CollisionEvents script = (CollisionEvents)target;

        serializedObject.Update();

        GUILayout.Space(5);

        // Draw Tag Filter field
        EditorGUILayout.PropertyField(
            serializedObject.FindProperty("requiredTag")
        );

        GUILayout.Space(10);

        if (GUILayout.Button("Add Event"))
        {
            showMenu = !showMenu;
        }

        if (showMenu)
        {
            GUILayout.Label("Select Event", EditorStyles.boldLabel);

            if (GUILayout.Button("OnCollisionEnter"))
                script.showCollisionEnter = true;

            if (GUILayout.Button("OnCollisionStay"))
                script.showCollisionStay = true;

            if (GUILayout.Button("OnCollisionExit"))
                script.showCollisionExit = true;

            if (GUILayout.Button("OnTriggerEnter"))
                script.showTriggerEnter = true;

            if (GUILayout.Button("OnTriggerStay"))
                script.showTriggerStay = true;

            if (GUILayout.Button("OnTriggerExit"))
                script.showTriggerExit = true;
        }

        GUILayout.Space(10);

        if (script.showCollisionEnter)
            EditorGUILayout.PropertyField(serializedObject.FindProperty("OnCollisionEnterEvent"));

        if (script.showCollisionStay)
            EditorGUILayout.PropertyField(serializedObject.FindProperty("OnCollisionStayEvent"));

        if (script.showCollisionExit)
            EditorGUILayout.PropertyField(serializedObject.FindProperty("OnCollisionExitEvent"));

        if (script.showTriggerEnter)
            EditorGUILayout.PropertyField(serializedObject.FindProperty("OnTriggerEnterEvent"));

        if (script.showTriggerStay)
            EditorGUILayout.PropertyField(serializedObject.FindProperty("OnTriggerStayEvent"));

        if (script.showTriggerExit)
            EditorGUILayout.PropertyField(serializedObject.FindProperty("OnTriggerExitEvent"));

        serializedObject.ApplyModifiedProperties();
    }
}