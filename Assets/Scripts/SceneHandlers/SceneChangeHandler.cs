using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeHandler : IGameService
{
    public SceneType PreviousScene { get; private set; } = SceneType.None;

    public void ChangeScene(SceneType newScene)
    {
        switch (newScene)
        {
            case SceneType.HeroSelection:
                ToHeroSelectionScene();
                break;
            case SceneType.Battle:
                ToBattleScene();
                break;
            default:
                throw new NotImplementedException("SceneType", newScene.ToString());
        }
    }

    private void ToHeroSelectionScene()
    {
        DataHandler dataHandler = ServiceLocator.Instance.Get<DataHandler>();
        Dictionary<int, IHero> playerHeroes = dataHandler.PlayerData.Heroes;

        IncrementNumberOfBattles(dataHandler);
        UpdatePlayerStats(dataHandler);
        IncrementPlayerHeroes(dataHandler);

        SetPreviousScene(SceneType.Battle);
        ServiceLocator.Instance.Get<DataHandler>().SavePlayerData();
        SceneManager.LoadScene("HeroSelection");
    }

    private async void ToBattleScene()
    {
        SelectedHeroesAsset selectedHeroesAsset = await ServiceLocator.Instance.Get<ScriptableObjectHandler>().GetSelectedHeroesAsset();

        HeroSelectionHandler heroSelectionHandler = ServiceLocator.Instance.Get<HeroSelectionHandler>();
        selectedHeroesAsset.SelectedHeroes = heroSelectionHandler.GetSelectedHeros();

        SetPreviousScene(SceneType.HeroSelection);
        SceneManager.LoadScene("Battle");
    }

    private void IncrementNumberOfBattles(DataHandler dataHandler)
    {
        dataHandler.PlayerData.NumberOfBattles++;
    }

    private void UpdatePlayerStats(DataHandler dataHandler)
    {
        foreach (KeyValuePair<int, IHero> item in dataHandler.PlayerData.Heroes)
        {
            IHero hero = item.Value;
            if (hero.CurrentHealth == 0) continue;

            hero.UpdateStats(hero.Experience + 1);
        }
    }

    private void IncrementPlayerHeroes(DataHandler dataHandler)
    {
        Dictionary<int, IHero> playerHeroes = dataHandler.PlayerData.Heroes;

        if (playerHeroes.Count < 10 && dataHandler.PlayerData.NumberOfBattles % 5 == 0)
        {
            IHero hero = HeroFactory.CreateRandomHero(dataHandler.GameData.Heroes, playerHeroes.Keys.ToList());
            dataHandler.PlayerData.Heroes.Add(hero.Id, hero);
        }
    }

    private void SetPreviousScene(SceneType sceneType)
    {
        PreviousScene = sceneType;
    }

}