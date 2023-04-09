using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AttackFactory
{
    public static T CreateAttack<T>(IActor attacker) where T : IAttack, new()
    {
        T attack = new T();
        attack.WithAttacker(attacker);

        return attack;
    }

    public static void CreateDamageEffect(IAttack attack)
    {
        // we have only one effect at the moment
        AssetReferenceGameObject prefabReference = new AssetReferenceGameObject($"Effects/FireDamage.prefab");

        if (prefabReference == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find asset reference for {attack.Name} attack");
        }

        IActor target = attack.Target;

        if (target == null)
        {
            ConsoleLog.Error(LogCategory.General, $"We need a target for this {attack.Name} attack");
        }

        ITile attackContainer = BattleCanvasController.Instance.GetTile(target);

        AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(prefabReference, attackContainer.GetEffectContainer());
        handle.Completed += (o) =>
        {
            if (o.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject damageGO = o.Result;
                HandleBattleMoveEffectGameObjectCompleted(damageGO, attack);
            }
            else
            {
                Debug.LogError($"Failed to instantiate damage game object {attack.Name}");
            }
        };
    }

    public static void CreateAttackEffect(IAttack attack)
    {
        // we have only one effect at the moment
        AssetReferenceGameObject prefabReference = new AssetReferenceGameObject($"Effects/AttackEffect.prefab");

        if (prefabReference == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find asset reference for {attack.Name} attack");
        }

        ITile attackContainer = BattleCanvasController.Instance.GetTile(attack.Attacker);

        AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(prefabReference, attackContainer.GetEffectContainer());
        handle.Completed += (o) =>
        {
            if (o.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject attackGO = o.Result;
                HandleBattleMoveEffectGameObjectCompleted(attackGO, attack);
            }
            else
            {
                Debug.LogError($"Failed to instantiate damage game object {attack.Name}");
            }
        };
    }

    private static void HandleBattleMoveEffectGameObjectCompleted(GameObject effectGO, IAttack attack)
    {
        IBattleMoveEffect battleMoveEffect = effectGO.GetComponent<IBattleMoveEffect>();
        battleMoveEffect.Setup(attack);
        battleMoveEffect.Initialise();
    }
}
