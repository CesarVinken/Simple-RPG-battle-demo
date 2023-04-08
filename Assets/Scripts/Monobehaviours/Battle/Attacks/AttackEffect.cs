using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackEffect : MonoBehaviour, IBattleMoveEffect
{
    private IAttack _attack;
    [SerializeField] private Image _image;

    public void Setup(IAttack attack)
    {
        if(_image == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could not find image");
        }

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
        _image.enabled = false;

        float waitTime = .6f;
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
