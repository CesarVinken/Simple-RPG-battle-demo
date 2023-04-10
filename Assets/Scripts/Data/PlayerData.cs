using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player data includes all the data that will change during the game. That includes for example which heroes the player has and how they have progressed
/// </summary>
public class PlayerData
{
    public Dictionary<int, IHero> Heroes = new Dictionary<int, IHero>();
    public int NumberOfBattles;
}
