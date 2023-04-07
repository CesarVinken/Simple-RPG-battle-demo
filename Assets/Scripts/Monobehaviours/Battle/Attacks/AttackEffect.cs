using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour, IAttackEffect
{
    private IAttack _attack;

    public void Setup(IAttack attack)
    {
        _attack = attack;
    }

    public void Initialise()
    {

    }

    public void OnAttackFinished()
    {
        _attack.Target.TakeDamage(_attack.Attacker.AttackPower);

        if (_attack.Target.CurrentHealth == 0) return;

        if(_attack.Attacker is IHero)
        {
            IAttack attack = AttackFactory.CreateAttack<DefaultAttack>(_attack.Target);
            BattleHandler battleHandler = BattleCanvasController.Instance.BattleHandler; // TODO use service locator 
            battleHandler.Attack(attack);
        }

        Destroy(gameObject);
    }
}
