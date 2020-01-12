using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class DataEditor : EditorWindow
{

    public GameData data;

    private string pathToData = "/StreamingAssets/data.json";

    [MenuItem("Window/Data Editor")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(DataEditor)).Show();
    }

    void OnGUI()
    {
        if (data != null)
        {
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("data");
            EditorGUILayout.PropertyField(serializedProperty, true);

            serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("Save data"))
            {
                SaveGameData();
            }
        }

        if (GUILayout.Button("Load data"))
        {
            LoadData();
        }
    }

    private void LoadData()
    {
        string pathToFile = Application.dataPath + pathToData;

        if (File.Exists(pathToFile))
        {
            string jsonData = File.ReadAllText(pathToFile);
            data = JsonUtility.FromJson<GameData>(jsonData);
        }
        else
        {
            data = new GameData();
        }
    }

    private void SaveGameData()
    {

        string jsonData = JsonUtility.ToJson(data);

        string filePath = Application.dataPath + pathToData;
        File.WriteAllText(filePath, jsonData);

    }
}