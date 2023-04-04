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

    public void SavePlayerData()
    {
        SerialisablePlayerData serialisablePlayerData = new SerialisablePlayerData()
            .WithPlayerHeroes(GameManager.Instance.GetHeroes());

        JsonPlayerDataWriter.GetInstance().SerialiseData(serialisablePlayerData);
        ConsoleLog.Log(LogCategory.General, $"Saved player data. {serialisablePlayerData.Heroes.Count} heros saved");
    }

    public PlayerData LoadPlayerData()
    {
        SerialisablePlayerData serialisablePlayerData = JsonPlayerDataReader.GetInstance().ReadData<SerialisablePlayerData>();

        if(serialisablePlayerData == null)
        {
            ConsoleLog.Log(LogCategory.General, $"Could not find any saved player data and created new data.");
            return new PlayerData();
        }

        ConsoleLog.Log(LogCategory.General, $"Loaded data for {serialisablePlayerData.Heroes.Count} player heros");

        return serialisablePlayerData.Deserialise();
    }
}
