using System.Collections.Generic;
using UnityEngine;

public class DataHandler : IGameService
{
    public GameData LoadGameData(GameData gameData)
    {
        SerialisableGameData serialisableGameData = JsonGameDataReader.GetInstance().ReadData<SerialisableGameData>();

        ConsoleLog.Log(LogCategory.Data, $"Loaded data for {serialisableGameData.Heroes.Count} hero blueprints");

        gameData = serialisableGameData.Deserialise();
        return gameData;
    }

    public void SavePlayerData(Dictionary<int, IHero> playerHeroes, int numberOfBattles)
    {
        SerialisablePlayerData serialisablePlayerData = new SerialisablePlayerData()
            .WithPlayerHeroes(playerHeroes)
            .WithNumberOfBattles(numberOfBattles);

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
