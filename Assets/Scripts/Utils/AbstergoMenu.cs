

using Newtonsoft.Json.Linq;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class AbstergoMenu
{
    public static DBOptionsPanel DBOptions;
    public static DebuggingPanel DebugOptions;

    [MenuItem("Abstergo/DB Panel")] //Para tocar del config de la base de datos
    public static void OpenDBPanel()
    {
        DBOptions = EditorWindow.GetWindow<DBOptionsPanel>();
        DBOptions.Show();
    }

    [MenuItem("Abstergo/Debugging")] //cosas de debugeo, como saltarse el login, o meter trucos, cosas asi
    public static void OpenDebugPanel()
    {
        DebugOptions = EditorWindow.GetWindow<DebuggingPanel>();
        DebugOptions.Show();
    }
}

public class DebuggingPanel : EditorWindow
{
    public bool skipLogin = false;

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Skip Login:", GUILayout.Width(100));
        skipLogin = EditorGUILayout.Toggle(skipLogin);
        EditorGUILayout.EndHorizontal();
    }
}

public class DBOptionsPanel : EditorWindow
{
    public string dbUrl = "";

    private void OnEnable()
    {
        // Read the JSON data from the file
        string filePath = Path.Combine(Application.dataPath, "DBConfig.json");
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);

            // Parse the JSON data into a JSONObject
            JObject json = JObject.Parse(jsonString);
            Debug.Log(json);

            // Retrieve the database path from the JSONObject
            if (json.ContainsKey("URL"))
            {
                dbUrl = json["URL"].Value<string>();
            }
        }
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Database URL:", GUILayout.Width(100));
        dbUrl = EditorGUILayout.TextField(dbUrl);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(100);

        if (GUILayout.Button("Apply Changes"))
        {
            ApplyChanges();
        }

    }
    private void ApplyChanges()
    {
        Debug.Log("Database URL changed to: " + dbUrl);

        JObject json = new JObject
        {
            { "URL", dbUrl }
        };

        string jsonString = json.ToString();

        string filePath = Path.Combine(Application.dataPath, "DBConfig.json");
        File.WriteAllText(filePath, jsonString);

    }
}