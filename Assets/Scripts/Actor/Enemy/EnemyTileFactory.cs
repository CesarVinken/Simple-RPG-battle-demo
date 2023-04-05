using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class EnemyTileFactory
{
    public static void CreateEnemyTile(Transform container, IEnemy enemy)
    {
        AssetReferenceGameObject prefabReference = new AssetReferenceGameObject("Assets/Prefabs/EnemyTile.prefab");
        AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(prefabReference, container);
        handle.Completed += (o) =>
        {
            if (o.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject enemyTileGO = o.Result;
                HandleEnemyTileLoadCompleted(enemyTileGO, enemy);
            }
            else
            {
                Debug.LogError($"Failed to instantiate tile for hero {enemy.Name}");
            }
        };
    }

    public static async Task<Sprite> LoadEnemyAvatar(IEnemy enemy)
    {
        AssetReferenceGameObject prefabReference = new AssetReferenceGameObject($"Assets/Sprites/Enemies/{enemy.Id}.png");

        if (prefabReference == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find an avatar asset for enemy {enemy.Id} {enemy.Name}");
        }

        AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(prefabReference);

        await handle.Task;

        return handle.Result;
    }

    private static void HandleEnemyTileLoadCompleted(GameObject heroSelectionTileGO, IEnemy enemy)
    {
        IEnemyTile enemyTile = heroSelectionTileGO.GetComponent<IEnemyTile>();
        enemyTile.Setup(enemy);
        enemyTile.Initialise();
    }
}
