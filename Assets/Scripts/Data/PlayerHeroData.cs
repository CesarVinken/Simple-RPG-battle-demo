using System.Collections.Generic;
using UnityEngine;

public struct PlayerHeroData
{
    public int Id;
    public int Experience;
}

public struct PlayerData
{
    public List<PlayerHeroData> Heroes;
    public int NumberOfBattles;
}