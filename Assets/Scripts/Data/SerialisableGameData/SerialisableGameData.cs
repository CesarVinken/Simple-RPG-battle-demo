using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerialisableGameData
{
    public List<SerialisableHeroBlueprint> Heroes = new List<SerialisableHeroBlueprint>();

    public GameData Deserialise()
    {
        Dictionary<int, HeroBlueprint> heroBlueprints = new Dictionary<int, HeroBlueprint>();

        for (int i = 0; i < Heroes.Count; i++)
        {
            SerialisableHeroBlueprint serialisableHeroBlueprint = Heroes[i];
            HeroBlueprint heroBlueprint = serialisableHeroBlueprint.Deserialise();
            heroBlueprints.Add(heroBlueprint.Id, heroBlueprint);
        }

        GameData gameData = new GameData().WithHeroes(heroBlueprints);

        return gameData;
    }
}
