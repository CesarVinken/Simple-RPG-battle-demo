using UnityEngine;

public class DataHandler
{
    private static readonly DataHandler _instance = new DataHandler();

    private DataHandler()
    {

    }

    public static DataHandler GetInstance()
    {
        return _instance;
    }

    public GameData LoadGameData()
    {
        SerialisableGameData serialisableGameData = JsonGameDataReader.GetInstance().ReadData<SerialisableGameData>();

        ConsoleLog.Log(LogCategory.General, $"Loaded data for {serialisableGameData.Heroes.Count} hero blueprints");

        return serialisableGameData.Deserialise();
    }
}
