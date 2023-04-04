using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class HeroTileFactory
{
    public static void CreateHeroTile(Transform container, IHero hero) 
    {
        AssetReferenceGameObject prefabReference = new AssetReferenceGameObject("Assets/Prefabs/HeroSelectionTile.prefab");
        AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(prefabReference, container);
        handle.Completed += (o) =>
        {
            if (o.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject heroSelectionTileGO = o.Result;
                HandleLoadCompleted(heroSelectionTileGO, hero);
            }
            else
            {
                Debug.LogError($"Failed to instantiate tile for hero {hero.Name}");
            }
        };
    }

    private static void HandleLoadCompleted(GameObject heroSelectionTileGO, IHero hero)
    {
        HeroSelectionTile heroSelectionTile = heroSelectionTileGO.GetComponent<HeroSelectionTile>();
        heroSelectionTile.Setup(hero);
        heroSelectionTile.Initialise();
    }
}