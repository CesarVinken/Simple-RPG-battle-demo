using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class BattleHandler
{
    IEnemyTile _enemyTile;
    List<IHeroTile> _heroTiles = new List<IHeroTile>();
    private static readonly System.Random _random = new System.Random();

    public BattleHandler(List<ITile> tiles)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            ITile tile = tiles[i];
            if (tile is IEnemyTile)
            {
                _enemyTile = tile as IEnemyTile;
            }
            else if(tile is IHeroTile)
            {
                _heroTiles.Add(tile as IHeroTile);
            }
        }

        if (_enemyTile == null)
        {
            ConsoleLog.Error(LogCategory.General, $"We need at least one enemy in the battle");
        }
    }

    public void Attack(IAttack attack)
    {
        IActor attacker = attack.Attacker;

        if (attacker is IHero)
        {
            attack.WithTarget(_enemyTile.GetActor());

        }
        else if (attacker is IEnemy)
        {
            List<IHeroTile> livingHeroes = _heroTiles.Where(h => h.Hero.CurrentHealth > 0).ToList();
            IHeroTile randomHeroTile = livingHeroes[_random.Next(_heroTiles.Count)];
            attack.WithTarget(randomHeroTile.GetActor());
        }
        else
        {
            ConsoleLog.Error(LogCategory.General, $"{attacker.Name} is of an unexpected type {attacker.GetType()}");
        }

        AttackFactory.CreateAttackEffect(attack);
    }


}
