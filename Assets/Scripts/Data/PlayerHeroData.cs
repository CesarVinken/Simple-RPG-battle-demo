using System.Collections.Generic;
using UnityEngine;

public struct PlayerHeroData
{
    public int Id;
    public int XP;
}

public struct PlayerData
{
    public List<PlayerHeroData> Heroes;

}