using System.Collections.Generic;
using UnityEngine;

public class SpawnpointContainer : MonoBehaviour
{
    [SerializeField] private Transform _heroSpawnpoint1;
    [SerializeField] private Transform _heroSpawnpoint2;
    [SerializeField] private Transform _heroSpawnpoint3;
    [SerializeField] private Transform _enemySpawnpoint;

    private List<Transform> _heroSpawnpoints = new List<Transform>();
    private Dictionary<IActor, Transform> _spawnpointsByActor = new Dictionary<IActor, Transform>();

    public void Setup()
    {
        if(_heroSpawnpoint1 == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find hero spawnpoint 1");
        }
        if(_heroSpawnpoint2 == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find hero spawnpoint 2");
        }
        if(_heroSpawnpoint3 == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find hero spawnpoint 3");
        }
        if(_enemySpawnpoint == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find enemy spawnpoint 3");
        }

        _heroSpawnpoints.Add(_heroSpawnpoint1);
        _heroSpawnpoints.Add(_heroSpawnpoint2);
        _heroSpawnpoints.Add(_heroSpawnpoint3);
    }

    public void Initialise(List<IHero> heroes, IEnemy enemy)
    {
        SpawnHeroes(heroes);
        SpawnEnemy(enemy);
    }

    private void SpawnHeroes(List<IHero> heroes)
    {
        if(heroes.Count != 3)
        {
            ConsoleLog.Error(LogCategory.General, $"We need 3 heroes to fight a battle, but we found {heroes.Count} heroes");
        }
        if(_heroSpawnpoints.Count != 3)
        {
            ConsoleLog.Error(LogCategory.General, $"We need 3 hero spawnpoints but we found {_heroSpawnpoints.Count} spawnpoints");
        }

        for (int i = 0; i < heroes.Count; i++)
        {
            IHero hero = heroes[i];
            Transform spawnpoint = _heroSpawnpoints[i];
            HeroTileFactory.CreateHeroBattleTile(spawnpoint, hero);
            _spawnpointsByActor.Add(hero, spawnpoint);
        }
    }

    private void SpawnEnemy(IEnemy enemy)
    {
        EnemyTileFactory.CreateEnemyBattleTile(_enemySpawnpoint, enemy);
        _spawnpointsByActor.Add(enemy, _enemySpawnpoint);
    }

    public void Register()
    {

    }

    //public Transform GetActorSpawnpoint(IActor actor)
    //{
    //    if(_spawnpointsByActor.TryGetValue(actor, out Transform spawnpoint))
    //    {
    //        return spawnpoint;
    //    }

    //    ConsoleLog.Error(LogCategory.General, $"Could not find a spawnpoint for {actor.Name}");
    //    return null;
    //}
}
