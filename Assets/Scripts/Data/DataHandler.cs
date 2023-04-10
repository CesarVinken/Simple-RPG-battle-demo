using System.Collections.Generic;
using UnityEngine;

public class DataHandler : IGameService
{
    public GameData GameData { get; private set; }
    public PlayerData PlayerData { get; private set; }

    public DataHandler()
    {
        GameData = new GameData();
        PlayerData = new PlayerData();
    }

    public void LoadGameData()
    {
        SerialisableGameData serialisableGameData = JsonGameDataReader.GetInstance().ReadData<SerialisableGameData>();

        ConsoleLog.Log(LogCategory.Data, $"Loaded data for {serialisableGameData.Heroes.Count} hero blueprints");

        GameData = serialisableGameData.Deserialise();
    }

    public void SavePlayerData()
    {
        SerialisablePlayerData serialisablePlayerData = new SerialisablePlayerData()
            .WithPlayerHeroes(PlayerData.Heroes)
            .WithNumberOfBattles(PlayerData.NumberOfBattles);

        JsonPlayerDataWriter.GetInstance().SerialiseData(serialisablePlayerData);
        ConsoleLog.Log(LogCategory.General, $"Saved player data. {serialisablePlayerData.Heroes.Count} heros saved");
    }

    public void LoadPlayerData()
    {
        SerialisablePlayerData serialisablePlayerData = JsonPlayerDataReader.GetInstance().ReadData<SerialisablePlayerData>();

        if(serialisablePlayerData == null)
        {
            ConsoleLog.Log(LogCategory.General, $"Could not find any saved player data and created new data.");
            PlayerData = new PlayerData();
        }

        ConsoleLog.Log(LogCategory.General, $"Loaded data for {serialisablePlayerData.Heroes.Count} player heros");

        PlayerData = serialisablePlayerData.Deserialise();
    }
}
