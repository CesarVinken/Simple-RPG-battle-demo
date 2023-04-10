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

        ICanvasController canvasController = ServiceLocator.Instance.Get<ICanvasController>();
        ITile targettedTile = canvasController.GetTile(_attack.Target);

        // Once an enemy has taken damage, the enemy will attack back
        if (targettedTile is IEnemyBattleTile)
        {
            canvasController.ActivateTile(targettedTile);
        }

        Destroy(gameObject);
    }
}
