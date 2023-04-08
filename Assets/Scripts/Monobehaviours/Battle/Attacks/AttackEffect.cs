using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour, IBattleMoveEffect
{
    private IAttack _attack;

    public void Setup(IAttack attack)
    {
        _attack = attack;
    }

    public void Initialise()
    {

    }

    public void OnEffectFinished()
    {
         StartCoroutine(WaitAndExecuteNextAttackPhase());
    }

    private IEnumerator WaitAndExecuteNextAttackPhase()
    {
        float waitTime = 1f;
        yield return new WaitForSeconds(waitTime);

        AttackHandler attackHandler = BattleCanvasController.Instance.CurrentAttackHandler;

        if (attackHandler == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find attack handler");
        }

        attackHandler.ExecutePhase(AttackPhase.TakingDamage);
        Destroy(gameObject);
    }
}
