using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class TileEffectFactory
{
    public static void CreateDamageValueEffect(IAttack attack)
    {
        AssetReferenceGameObject prefabReference = new AssetReferenceGameObject($"Effects/DamageValueEffect.prefab");

        if (prefabReference == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find asset reference for damage value prefab");
        }

        IBattleTile targetTile = BattleCanvasController.Instance.GetTile(attack.Target) as IBattleTile;

        if(targetTile == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find an target tile");
        }

        AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(prefabReference, targetTile.GetDamageValueEffectContainer());
        handle.Completed += (o) =>
        {
            if (o.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject damageGO = o.Result;
                HandleDamageValueEffectGameObjectCompleted(damageGO, attack.Attacker);
            }
            else
            {
                Debug.LogError($"Failed to instantiate damage game object {attack.Attacker.Name}");
            }
        };
    }

    private static void HandleDamageValueEffectGameObjectCompleted(GameObject effectGO, IActor attacker)
    {
        DamageValueEffect attackValueEffect = effectGO.GetComponent<DamageValueEffect>();
        attackValueEffect.Setup(attacker);
        attackValueEffect.Initialise();
    }
}
