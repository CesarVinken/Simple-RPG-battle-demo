using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class HeroTileFactory
{
    public static void CreateHeroSelectionTile(Transform container, IHero hero)
    {
        AssetReferenceGameObject prefabReference = new AssetReferenceGameObject("Tiles/HeroSelectionTile.prefab");
        AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(prefabReference, container);
        handle.Completed += (o) =>
        {
            if (o.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject heroSelectionTileGO = o.Result;
                HandleHeroTileLoadCompleted(heroSelectionTileGO, hero);
            }
            else
            {
                Debug.LogError($"Failed to instantiate tile for hero {hero.Name}");
            }
        };
    }

    public static void CreateHeroBattleTile(Transform container, IHero hero)
    {
        AssetReferenceGameObject prefabReference = new AssetReferenceGameObject("Tiles/HeroBattleTile.prefab");
        AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(prefabReference, container);
        handle.Completed += (o) =>
        {
            if (o.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject heroBattleTileGO = o.Result;
                HandleHeroTileLoadCompleted(heroBattleTileGO, hero);
            }
            else
            {
                Debug.LogError($"Failed to instantiate tile for hero {hero.Name}");
            }
        };
    }

    public static async Task<Sprite> LoadHeroAvatar(IHero hero)
    {
        AssetReferenceGameObject prefabReference = new AssetReferenceGameObject($"Heroes/{hero.Id}.png");

        if(prefabReference == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find an avatar asset for hero {hero.Id} {hero.Name}");
        }

        AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(prefabReference);

        await handle.Task;

        return handle.Result;
    }

    private static void HandleHeroTileLoadCompleted(GameObject heroTileGO, IHero hero)
    {      
        IHeroTile heroSelectionTile = heroTileGO.GetComponent<IHeroTile>();
        heroSelectionTile.Setup(hero);
        heroSelectionTile.Initialise();
    }
}