using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AttackHandler : IGameService
{
    private IAttack _attack;
    private static readonly System.Random _random = new System.Random();

    public void SetAttack(IAttack attack)
    {
        _attack = attack;
    }

    public IAttack GetCurrentAttack()
    {
        return _attack;
    }

    public IAttack SetTarget(List<ITile> possibleTargets)
    {
        List<ITile> livingTargets = possibleTargets.Where(h => h.GetActor().CurrentHealth > 0).ToList();

        if(livingTargets.Count == 0)
        {
            ConsoleLog.Error(LogCategory.General, $"We expect at least one of the targets to be alive");
        }

        ITile randomTile = livingTargets[_random.Next(livingTargets.Count)];
        _attack.WithTarget(randomTile.GetActor());
      
        return _attack;
    }

    public void ExecutePhase(AttackPhase attackPhase)
    {
        switch (attackPhase)
        {
            case AttackPhase.Attacking:
                AttackFactory.CreateAttackEffect(_attack);
                break;
            case AttackPhase.TakingDamage:
                AttackFactory.CreateDamageEffect(_attack);
                TileEffectFactory.CreateDamageValueEffect(_attack);
                break;
            default:
                break;
        }
    }
}
