using System;
using System.IO;
using UnityEngine;

public class JsonGameDataReader : IJsonFileReader
{
    private static readonly JsonGameDataReader _instance = new JsonGameDataReader();
    
    private JsonGameDataReader()
    {

    }

    public static JsonGameDataReader GetInstance()
    {
        return _instance;
    }

    public SerialisableGameData ReadData<SerialisableGameData>()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "game.json");

        if (!File.Exists(filePath))
        {
            throw new Exception($"File game.json doesn't exist.");
        }

        string jsonContent = File.ReadAllText(filePath);

        SerialisableGameData gameData = JsonUtility.FromJson<SerialisableGameData>(jsonContent);

        return gameData;
    }
}
