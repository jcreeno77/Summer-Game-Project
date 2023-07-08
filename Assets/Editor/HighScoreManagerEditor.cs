#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HighScoreManager))]
public class HighScoreManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draws the default inspector

        HighScoreManager HighScoreManager = (HighScoreManager)target;

        if (GUILayout.Button("Clear High Scores")) // Adds a button to clear high scores
        {
            HighScoreManager.ClearHighScores();
        }
    }
}
#endif
