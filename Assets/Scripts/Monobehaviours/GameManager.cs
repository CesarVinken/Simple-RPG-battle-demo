using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameData GameData { get; private set; }

    private Dictionary<int, IHero> _playerHeroes = new Dictionary<int, IHero>();
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

        PreviousScene = SceneType.None;
    }

    public void Start()
    {
        GameData = DataHandler.GetInstance().LoadGameData();

        string sceneName = SceneManager.GetActiveScene().name;

        IHero hero1 = HeroFactory.CreateRandomHero();
        _playerHeroes.Add(hero1.Id, hero1);
        IHero hero2 = HeroFactory.CreateRandomHero();
        _playerHeroes.Add(hero2.Id, hero2);
        IHero hero3 = HeroFactory.CreateRandomHero();
        _playerHeroes.Add(hero3.Id, hero3);

        if (PreviousScene == SceneType.None && sceneName == "HeroSelection") // this means this is a start up of the Level scene directly in Unity and we never selected a current level in the menu
        {
            if (HeroSelectionCanvasController.Instance == null)
            {
                ConsoleLog.Error(LogCategory.General, $"Could not find the Instance of the HeroSelectionCanvasController");
            }

            HeroSelectionCanvasController.Instance.Setup();
            HeroSelectionCanvasController.Instance.Initialise();
        }
    }

    public Dictionary<int, IHero> GetHeroes()
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
}
