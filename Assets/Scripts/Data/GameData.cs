using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public Dictionary<int, HeroBlueprint> Heroes = new Dictionary<int, HeroBlueprint>();
    public Dictionary<int, EnemyBlueprint> Enemies = new Dictionary<int, EnemyBlueprint>();

    public GameData WithHeroes(Dictionary<int, HeroBlueprint> heroes)
    {
        Heroes = heroes;

        return this;
    }

    public GameData WithEnemies(Dictionary<int, EnemyBlueprint> enemies)
    {
        Enemies = enemies;

        return this;
    }
}
