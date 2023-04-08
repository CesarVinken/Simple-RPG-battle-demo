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
        IBattleEndPanel battleEndPanel = battleEndPanelGO.GetComponent<IBattleEndPanel>();
        battleEndPanel.Setup();
        battleEndPanel.Initialise(hasWon);
    }

    public static void CreateHeroInfoPanel(InfoPanelContainer infoPanelContainer, IHero hero)
    {
        AssetReferenceGameObject prefabReference = new AssetReferenceGameObject("Assets/Prefabs/Panels/HeroInfoPanel.prefab");
        AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(prefabReference, infoPanelContainer.transform);
        handle.Completed += (o) =>
        {
            if (o.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject heroInfoGO = o.Result;
                HandleHeroInfoPanelLoadCompleted(infoPanelContainer, heroInfoGO, hero);
            }
            else
            {
                Debug.LogError($"Failed to instantiate hero info panel");
            }
        };
    }

    private static void HandleHeroInfoPanelLoadCompleted(InfoPanelContainer infoPanelContainer, GameObject heroInfoPanelGO, IHero hero)
    {
        IHeroInfoPanel heroInfoPanel = heroInfoPanelGO.GetComponent<IHeroInfoPanel>();
        heroInfoPanel.Setup();
        heroInfoPanel.Initialise(infoPanelContainer, hero);
    }
}