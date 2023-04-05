using System.IO;
using UnityEditor;
using UnityEngine;

public class PlayerDataResetter : EditorWindow
{
    public static void Reset()
    {
        string savePath = Path.Combine(Application.persistentDataPath, "save.json");

        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("Deleted player data");
        }
    }
}
