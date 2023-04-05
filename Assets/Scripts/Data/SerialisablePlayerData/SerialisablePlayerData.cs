using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SerialisablePlayerData holds all the data regarding our player's progress. 
/// We record what heroes the player has in their company. We use the hero's Id to link it to the data from the HeroBlueprint data. 
/// We also record the hero's xp. Based on this we can calculate their level, health and attack. For the game's current rules that should do. 
/// If other elements are introduced that influence eg. the player's base health other than xp, we should start recording more data
/// </summary>
[Serializable]
public class SerialisablePlayerData
{
    public List<SerialisablePlayerHero> Heroes = new List<SerialisablePlayerHero>();
    public int NumberOfBattles;

    public SerialisablePlayerData WithPlayerHeroes(Dictionary<int, IHero> heroesById)
    {
        Heroes.Clear();

        foreach (KeyValuePair<int, IHero> item in heroesById)
        {
            SerialisablePlayerHero serialisablePlayerHero = new SerialisablePlayerHero();
            serialisablePlayerHero.Id = item.Key;
            serialisablePlayerHero.XP = item.Value.XP;
            Heroes.Add(serialisablePlayerHero);
        }

        return this;
    }

    public SerialisablePlayerData WithNumberOfBattles(int numberOfBattles)
    {
        NumberOfBattles = numberOfBattles;

        return this;
    }

    public PlayerData Deserialise()
    {
        List<PlayerHeroData> heroes = new List<PlayerHeroData>();

        for (int i = 0; i < Heroes.Count; i++)
        {
            PlayerHeroData playerHeroData = new PlayerHeroData();
            playerHeroData.Id = Heroes[i].Id;
            playerHeroData.XP = Heroes[i].XP;
            heroes.Add(playerHeroData);
        }

        PlayerData playerData = new PlayerData();
        playerData.Heroes = heroes;
        playerData.NumberOfBattles = NumberOfBattles;

        return playerData;
    }
}

