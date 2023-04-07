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

    public static void CreateAttackEffect(IAttack attack)
    {
        AssetReferenceGameObject prefabReference = new AssetReferenceGameObject($"Assets/Prefabs/Attacks/{attack.Name}.prefab");

        if(prefabReference == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find asset reference for {attack.Name} attack");
        }

        IActor target = attack.Target;

        if(target == null)
        {
            ConsoleLog.Error(LogCategory.General, $"We need a target for this {attack.Name} attack");
        }

        ITile attackContainer = BattleCanvasController.Instance.GetTile(target);

        AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(prefabReference, attackContainer.GetTransform());
        handle.Completed += (o) =>
        {
            if (o.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject attackGO = o.Result;
                HandleAttackGameObjectCompleted(attackGO, attack);
            }
            else
            {
                Debug.LogError($"Failed to instantiate attack game object {attack.Name}");
            }
        };
    }

    private static void HandleAttackGameObjectCompleted(GameObject attackGO, IAttack attack)
    {
        IAttackEffect attackEffect = attackGO.GetComponent<IAttackEffect>();
        attackEffect.Setup(attack);
        attackEffect.Initialise();
    }
}
