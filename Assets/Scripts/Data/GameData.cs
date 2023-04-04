using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public Dictionary<int, HeroBlueprint> Heroes = new Dictionary<int, HeroBlueprint>();

    public GameData WithHeroes(Dictionary<int, HeroBlueprint> heroes)
    {
        Heroes = heroes;

        return this;
    }
}
