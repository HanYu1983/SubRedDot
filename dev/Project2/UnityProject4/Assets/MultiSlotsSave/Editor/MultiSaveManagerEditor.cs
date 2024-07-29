using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MultiSaveManager), true)]
public class MultiSaveManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button(new GUIContent("Delete Save Data")))
        {
            var manager = target as MultiSaveManager;

            if (manager != null)
            {
                manager.DeleteAllSavedGames();
            }
        }

        base.OnInspectorGUI();
    }
}
