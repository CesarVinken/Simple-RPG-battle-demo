using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerialisableGameData
{
    public List<SerialisableHeroBlueprint> Heroes = new List<SerialisableHeroBlueprint>();
    public List<SerialisableEnemyBlueprint> Enemies = new List<SerialisableEnemyBlueprint>();

    public GameData Deserialise()
    {
        Dictionary<int, HeroBlueprint> heroBlueprints = DeserialiseHeroes();
        Dictionary<int, EnemyBlueprint> enemyBlueprints = DeserialiseEnemies();

        GameData gameData = new GameData()
            .WithHeroes(heroBlueprints)
            .WithEnemies(enemyBlueprints);

        return gameData;
    }

    private Dictionary<int, EnemyBlueprint> DeserialiseEnemies()
    {
        Dictionary<int, EnemyBlueprint> enemyBlueprints = new Dictionary<int, EnemyBlueprint>();

        for (int i = 0; i < Enemies.Count; i++)
        {
            SerialisableEnemyBlueprint serialisableEnemyBlueprint = Enemies[i];
            EnemyBlueprint enemyBlueprint = serialisableEnemyBlueprint.Deserialise();
            enemyBlueprints.Add(enemyBlueprint.Id, enemyBlueprint);
        }

        return enemyBlueprints;
    }

    private Dictionary<int, HeroBlueprint> DeserialiseHeroes()
    {
        Dictionary<int, HeroBlueprint> heroBlueprints = new Dictionary<int, HeroBlueprint>();

        for (int i = 0; i < Heroes.Count; i++)
        {
            SerialisableHeroBlueprint serialisableHeroBlueprint = Heroes[i];
            HeroBlueprint heroBlueprint = serialisableHeroBlueprint.Deserialise();
            heroBlueprints.Add(heroBlueprint.Id, heroBlueprint);
        }

        return heroBlueprints;
    }
}
