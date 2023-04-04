//using System.IO;
//using UnityEngine;

//public class JsonConfigurationDataWriter : IJsonFileWriter
//{
//    private static readonly JsonConfigurationDataWriter _instance = new JsonConfigurationDataWriter();

//    private SerialisableConfigurationData _data;
//    private string _path;

//    private JsonConfigurationDataWriter()
//    {

//    }

//    public static JsonConfigurationDataWriter GetInstance()
//    {
//        return _instance;
//    }

//    public void SerialiseData<T>(T configurationData, string fileName)
//    {
//        Directory.CreateDirectory(Path.Combine(Application.dataPath, "StreamingAssets", "config"));

//        _data = configurationData as SerialisableConfigurationData;
//        _path = Path.Combine(Application.dataPath, "StreamingAssets", "config", fileName + ".json");

//        string jsonDataString = JsonUtility.ToJson(_data, true).ToString();

//        File.WriteAllText(_path, jsonDataString);
//    }
//}