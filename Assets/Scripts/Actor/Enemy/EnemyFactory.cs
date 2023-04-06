using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyFactory
{
    private static readonly System.Random _random = new System.Random();

    public static IEnemy CreateRandomEnemy()
    {
        List <EnemyBlueprint> enemyData = GameManager.Instance.GameData.Enemies.Values.ToList();

        EnemyBlueprint enemyBlueprint = enemyData[_random.Next(enemyData.Count)];

        IEnemy enemy = new Enemy(enemyBlueprint);
        return enemy;
    }
}
