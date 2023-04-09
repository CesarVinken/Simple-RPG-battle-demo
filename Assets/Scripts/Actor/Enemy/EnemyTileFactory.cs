using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class EnemyTileFactory
{
    public static void CreateEnemyBattleTile(Transform container, IEnemy enemy)
    {
        AssetReferenceGameObject prefabReference = new AssetReferenceGameObject("Tiles/EnemyBattleTile.prefab");
        AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(prefabReference, container);
        handle.Completed += (o) =>
        {
            if (o.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject enemyTileGO = o.Result;
                HandleEnemyBattleTileLoadCompleted(enemyTileGO, enemy);
            }
            else
            {
                Debug.LogError($"Failed to instantiate tile for enemy {enemy.Name}");
            }
        };
    }

    public static async Task<Sprite> LoadEnemyAvatar(IEnemy enemy)
    {
        AssetReferenceGameObject prefabReference = new AssetReferenceGameObject($"Enemies/{enemy.Id}.png");

        if (prefabReference == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find an avatar asset for enemy {enemy.Id} {enemy.Name}");
        }

        AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(prefabReference);

        await handle.Task;

        return handle.Result;
    }

    private static void HandleEnemyBattleTileLoadCompleted(GameObject enemyBattleTileGO, IEnemy enemy)
    {
        IEnemyTile enemyTile = enemyBattleTileGO.GetComponent<IEnemyTile>();
        enemyTile.Setup(enemy, BattleCanvasController.Instance); // TODO use ServiceLocator instead
        enemyTile.Initialise();
    }
}
