using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GUIManager))] // Attach to MyComponent
public class GUIManagerToolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default Inspector UI
        DrawDefaultInspector();

        // Reference to the target component
        GUIManager myComponent = (GUIManager)target;

        // Create a button in the Inspector
        if (GUILayout.Button("Execute DoSomething"))
        {
            myComponent.PlayEffectHurt(); // Call the method when clicked
        }
    }
}