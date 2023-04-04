using System;
using UnityEngine;

[Serializable]
public class SerialisablePlayerHero
{
    public int Id;
    public int XP;

    public PlayerHeroData Deserialise()
    {
        PlayerHeroData playerHeroData = new PlayerHeroData();
        playerHeroData.Id = Id;
        playerHeroData.XP = XP;

        return playerHeroData;
    }
}