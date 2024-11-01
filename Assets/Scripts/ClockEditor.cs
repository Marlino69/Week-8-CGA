using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Clock))]
public class ClockEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();

        // Get reference to the Clock script
        Clock clock = (Clock)target;

        // Add a space for clarity
        EditorGUILayout.Space();

        // Add a "Start Stopwatch" button
        if (GUILayout.Button("Start Stopwatch"))
        {
            clock.StartStopwatch();
        }

        // Add a "Stop Stopwatch" button
        if (GUILayout.Button("Stop Stopwatch"))
        {
            clock.StopStopwatch();
        }

        // Add a "Reset Stopwatch" button
        if (GUILayout.Button("Reset Stopwatch"))
        {
            clock.ResetStopwatch();
        }
    }
}
