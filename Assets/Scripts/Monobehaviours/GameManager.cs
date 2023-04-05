using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameData GameData { get; private set; }

    private Dictionary<int, IHero> _playerHeroes = new Dictionary<int, IHero>();
    private int _numberOfBattles = 0;

    // By default the PreviousScene is set to None. If we open the Battle scene directly from Unity (and not through HeroSelection), we can identify this through this property. In this case we load the Battle scene with default data.
    public SceneType PreviousScene { get; private set; } 

    public void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        SetPreviousScene(PreviousScene);
    }

    public void Start()
    {
        GameData = DataHandler.GetInstance().LoadGameData();
        PlayerData playerData = DataHandler.GetInstance().LoadPlayerData();

        SetUpPlayerData(playerData);

        // We need to make sure that we load all the data, and only than initialise the ui canvas controller
        if (PreviousScene == SceneType.None) 
        {
            string sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == "HeroSelection")
            {
                if (HeroSelectionCanvasController.Instance == null)
                {
                    ConsoleLog.Error(LogCategory.General, $"Could not find the Instance of the HeroSelectionCanvasController");
                }

                HeroSelectionCanvasController.Instance.Setup();
                HeroSelectionCanvasController.Instance.Initialise();
            }
            else
            {
                if (BattleCanvasController.Instance == null)
                {
                    ConsoleLog.Error(LogCategory.General, $"Could not find the Instance of the BattleCanvasController");
                }

                BattleCanvasController.Instance.Setup();
                BattleCanvasController.Instance.Initialise();
            }

        }
    }

    private void CreateInitialHeroes()
    {
        ConsoleLog.Log(LogCategory.Initialisation, $"Create the player's first 3 heroes");
        int numberOfHeroes = 3;
        for (int i = 0; i < numberOfHeroes; i++)
        {
            IHero hero = HeroFactory.CreateRandomHero(GameData.Heroes, GetPlayerHeroes().Keys.ToList());
            _playerHeroes.Add(hero.Id, hero);
        }
    }

    private void SetUpPlayerHeroes(PlayerData playerData)
    {
        // if we cannot find player data heroes, then it is the first time we load the game. In that case we will load the player's initial 3 heroes.
        if (playerData.Heroes == null)
        {
            CreateInitialHeroes();
            return;
        }

        for (int i = 0; i < playerData.Heroes.Count; i++)
        {
            PlayerHeroData playerHeroData = playerData.Heroes[i];
            IHero hero = HeroFactory.CreateHero(playerHeroData.Id);
            hero.UpdateStats(playerHeroData.XP);
            _playerHeroes.Add(hero.Id, hero);
        }
    }

    private void SetUpPlayerData(PlayerData playerData)
    {
        SetNumberOfBattles(playerData.NumberOfBattles);

        SetUpPlayerHeroes(playerData);
    }

    public Dictionary<int, IHero> GetPlayerHeroes()
    {
        return _playerHeroes;
    }

    public IHero GetHero(int id)
    {
        if(_playerHeroes.TryGetValue(id, out IHero hero))
        {
            return hero;
        }
        return null;
    }

    private void SetPreviousScene(SceneType sceneType)
    {
        PreviousScene = sceneType;
    }

    public void ToScene(SceneType sceneType)
    {
        switch (sceneType)
        {
            case SceneType.HeroSelection:
                SetNumberOfBattles(_numberOfBattles + 1);
                Dictionary<int, IHero> heroes = GetPlayerHeroes();

                // Later move this to the victory screen. Heroes should be updated in the victory screen phase
                foreach (KeyValuePair<int, IHero> item in heroes)
                {
                    IHero hero = item.Value;
                    if (hero.Health == 0) continue;

                    hero.UpdateStats(hero.XP + 1);
                }

                SetPreviousScene(SceneType.Battle);
                DataHandler.GetInstance().SavePlayerData(heroes, _numberOfBattles);
                SceneManager.LoadScene("HeroSelection");
                break;
            case SceneType.Battle:
                SetPreviousScene(SceneType.HeroSelection);
                SceneManager.LoadScene("Battle");
                break;
            default:
                throw new NotImplementedException("SceneType", sceneType.ToString());
        }
    }

    private void SetNumberOfBattles(int numberOfBattles)
    {
        _numberOfBattles = numberOfBattles;

        ConsoleLog.Log(LogCategory.General, $"Number of battles is now {_numberOfBattles}");
    }
}
