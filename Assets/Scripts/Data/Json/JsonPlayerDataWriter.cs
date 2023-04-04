using System.IO;
using UnityEngine;

public class JsonPlayerDataWriter : IJsonFileWriter
{
    private static readonly JsonPlayerDataWriter _instance = new JsonPlayerDataWriter();

    private SerialisablePlayerData _data;
    private string _path;

    private JsonPlayerDataWriter()
    {

    }

    public static JsonPlayerDataWriter GetInstance()
    {
        return _instance;
    }

    public void SerialiseData<T>(T configurationData)
    {
        _data = configurationData as SerialisablePlayerData;
        _path = Path.Combine(Application.persistentDataPath, "save.json");

        string jsonDataString = JsonUtility.ToJson(_data, true).ToString();

        File.WriteAllText(_path, jsonDataString);
    }
}