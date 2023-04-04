using System;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class JsonPlayerDataReader : IJsonFileReader
{
    private static readonly JsonPlayerDataReader _instance = new JsonPlayerDataReader();
    
    private JsonPlayerDataReader()
    {

    }

    public static JsonPlayerDataReader GetInstance()
    {
        return _instance;
    }

    public SerialisablePlayerData ReadData<SerialisablePlayerData>()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "save.json");
        string jsonContent;

        if (!File.Exists(filePath))
        {
            ConsoleLog.Warning(LogCategory.General, $"There is no save.json file with saved player data");
            jsonContent = "";
        }
        else
        {
            jsonContent = File.ReadAllText(filePath);
        }

        SerialisablePlayerData gameData = JsonUtility.FromJson<SerialisablePlayerData>(jsonContent);
        return gameData;
    }
}
