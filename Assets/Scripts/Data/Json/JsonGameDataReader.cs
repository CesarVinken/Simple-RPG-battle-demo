using System;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class JsonGameDataReader : IJsonFileReader
{
    public SerialisableGameData ReadData<SerialisableGameData>()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "game.json");
        string jsonContent = GetJsonString(filePath);
        SerialisableGameData gameData = JsonUtility.FromJson<SerialisableGameData>(jsonContent);
        return gameData;
    }

    private string GetJsonString(string filePath)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            UnityWebRequest loadingRequest = UnityWebRequest.Get(filePath);
            loadingRequest.SendWebRequest();
            while (!loadingRequest.isDone) ;

            return loadingRequest.downloadHandler.text.Trim();
        }
        else
        {
            if (!File.Exists(filePath))
            {
                throw new Exception($"File game.json doesn't exist.");
            }
            return File.ReadAllText(filePath);
        }
    }
}
