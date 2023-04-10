using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ScriptableObjectHandler : IGameService
{
    private SelectedHeroesAsset _selectedHeroesAsset;

    public async Task<SelectedHeroesAsset> GetSelectedHeroesAsset()
    {
        if (_selectedHeroesAsset == null)
        {
            _selectedHeroesAsset = await LoadSelectedHeroAsset();

            // we started the battle scene directly from Unity inspector, we never selected heroes. We need to pick 3 random heroes
            if (_selectedHeroesAsset.SelectedHeroes.Count < 3) 
            {
                _selectedHeroesAsset.SelectedHeroes = SelectRandomPlayerHeroes();
            }
        }

        return _selectedHeroesAsset;
    }

    public async Task<SelectedHeroesAsset> LoadSelectedHeroAsset()
    {
        AsyncOperationHandle<SelectedHeroesAsset> handle = Addressables.LoadAssetAsync<SelectedHeroesAsset>("ScriptableObjects/SelectedHeroes.asset");

        while (!handle.IsDone)
        {
            await Task.Yield();
        }

        return handle.Result;
   
    }

    private List<IHero> SelectRandomPlayerHeroes()
    {
        DataHandler dataHandler = ServiceLocator.Instance.Get<DataHandler>();
        List<IHero> playerHeroes = dataHandler.PlayerData.Heroes.Values.ToList();

        if (playerHeroes.Count < 3)
        {
            ConsoleLog.Error(LogCategory.General, $"The player needs at least 3 heroes but currently has only {playerHeroes.Count}");
        }

        List<IHero> selectedHeroes = new List<IHero>();

        for (int i = 0; i < 3; i++)
        {
            selectedHeroes.Add(playerHeroes[i]);
        }

        return selectedHeroes;
    }
}
