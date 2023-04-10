using System.IO;
using UnityEngine;

public class JsonPlayerDataWriter : IJsonFileWriter
{
    private SerialisablePlayerData _data;
    private string _path;

    public void SerialiseData<T>(T configurationData)
    {
        _data = configurationData as SerialisablePlayerData;
        _path = Path.Combine(Application.persistentDataPath, "save.json");

        string jsonDataString = JsonUtility.ToJson(_data, true).ToString();

        File.WriteAllText(_path, jsonDataString);
    }
}