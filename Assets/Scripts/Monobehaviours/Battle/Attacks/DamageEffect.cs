using UnityEngine;

public class DamageEffect : MonoBehaviour, IDamageEffect
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
        _attack.Target.TakeDamage(_attack.Attacker.AttackPower);

        if (_attack.Target.CurrentHealth == 0) return;

        ITile targettedTile = BattleCanvasController.Instance.GetTile(_attack.Target);

        // Once an enemey has taken damage, the enemy will attack back
        if (targettedTile is IEnemyBattleTile)
        {
            BattleCanvasController.Instance.TriggerAttack(targettedTile);
        }

        Destroy(gameObject);
    }
}
