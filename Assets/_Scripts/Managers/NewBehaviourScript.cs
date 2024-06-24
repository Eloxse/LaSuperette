using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(MonitoringController))]
public class VideoMonitoringEditor : Editor
{
    private MonitoringController _monitoringController;

    void Start()
    {
        _monitoringController = MonitoringController.Instance;
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MonitoringController m = (MonitoringController)target;

        // Draw a button to add a new video slot
        if (GUILayout.Button("Add Video Slot"))
        {
            m.AllVideos.Add(null);
        }

        // Draw a button to clear the list
        if (GUILayout.Button("Clear All Slots"))
        {
            m.AllVideos.Clear();
        }

        // Display each video slot with a dropdown to assign GameObjects
        for (int i = 0; i < m.AllVideos.Count; i++)
        {
            m.AllVideos[i] = (GameObject)EditorGUILayout.ObjectField("Video " + (i + 1), m.AllVideos[i], typeof(GameObject), true);
        }

        // Apply changes
        if (GUI.changed)
        {
            EditorUtility.SetDirty(m);
        }
    }
}
