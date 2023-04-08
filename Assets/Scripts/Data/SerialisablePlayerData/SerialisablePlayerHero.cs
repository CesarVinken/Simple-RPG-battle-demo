using System;
using UnityEngine;

[Serializable]
public class SerialisablePlayerHero
{
    public int Id;
    public int Experience;

    public PlayerHeroData Deserialise()
    {
        PlayerHeroData playerHeroData = new PlayerHeroData();
        playerHeroData.Id = Id;
        playerHeroData.Experience = Experience;

        return playerHeroData;
    }
}