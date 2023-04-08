using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class UIPanelFactory
{
    public static void CreateBattleEndPanel(Transform container, bool hasWon)
    {
        AssetReferenceGameObject prefabReference = new AssetReferenceGameObject("Assets/Prefabs/Battle/UI/BattleEndPanel.prefab");
        AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(prefabReference, container);
        handle.Completed += (o) =>
        {
            if (o.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject battleEndPanelGO = o.Result;
                HandleLevelEndPanelLoadCompleted(battleEndPanelGO, hasWon);
            }
            else
            {
                Debug.LogError($"Failed to instantiate battle end panel");
            }
        };
    }

    private static void HandleLevelEndPanelLoadCompleted(GameObject battleEndPanelGO, bool hasWon)
    {
        BattleEndPanel battleEndPanel = battleEndPanelGO.GetComponent<BattleEndPanel>();
        battleEndPanel.Setup();
        battleEndPanel.Initialise(hasWon);
    }

    public static void CreateHeroInfoPanel(Transform container, IHero hero)
    {
        AssetReferenceGameObject prefabReference = new AssetReferenceGameObject("Assets/Prefabs/Panels/HeroInfoPanel.prefab");
        AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(prefabReference, container);
        handle.Completed += (o) =>
        {
            if (o.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject heroInfoGO = o.Result;
                HandleHeroInfoPanelLoadCompleted(heroInfoGO, hero);
            }
            else
            {
                Debug.LogError($"Failed to instantiate hero info panel");
            }
        };
    }

    private static void HandleHeroInfoPanelLoadCompleted(GameObject heroInfoPanelGO, IHero hero)
    {
        HeroInfoPanel heroInfoPanel = heroInfoPanelGO.GetComponent<HeroInfoPanel>();
        heroInfoPanel.Setup();
        heroInfoPanel.Initialise(hero);
    }
}