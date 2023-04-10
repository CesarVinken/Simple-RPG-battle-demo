using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        ServiceLocator.Setup();
        ServiceLocator.Instance.Register<DataHandler>(new DataHandler());
        ServiceLocator.Instance.Register<ScriptableObjectHandler>(new ScriptableObjectHandler());
        ServiceLocator.Instance.Register<AttackHandler>(new AttackHandler());
        ServiceLocator.Instance.Register<GameEventHandler>(new GameEventHandler());
        ServiceLocator.Instance.Register<HeroSelectionHandler>(new HeroSelectionHandler());
        ServiceLocator.Instance.Register<PanelHandler>(new PanelHandler());
        ServiceLocator.Instance.Register<SceneChangeHandler>(new SceneChangeHandler());

        ServiceLocator.Instance.Register<JsonGameDataReader>(new JsonGameDataReader());
        ServiceLocator.Instance.Register<JsonPlayerDataReader>(new JsonPlayerDataReader());
        ServiceLocator.Instance.Register<JsonPlayerDataWriter>(new JsonPlayerDataWriter());


        DataHandler dataHandler = ServiceLocator.Instance.Get<DataHandler>();
        dataHandler.LoadGameData();
        dataHandler.LoadPlayerData();

        if (dataHandler.PlayerData.Heroes.Count == 0) // this happens the first time we start the game
        {
            CreateInitialHeroes();
        }
    }

    private void CreateInitialHeroes()
    {
        ConsoleLog.Log(LogCategory.Initialisation, $"Create the player's first 3 heroes");

        DataHandler dataHandler = ServiceLocator.Instance.Get<DataHandler>();
        int numberOfHeroes = 3;

        for (int i = 0; i < numberOfHeroes; i++)
        {
            IHero hero = HeroFactory.CreateRandomHero(dataHandler.GameData.Heroes, dataHandler.PlayerData.Heroes.Keys.ToList());
            dataHandler.PlayerData.Heroes.Add(hero.Id, hero);
        }
    }
}
