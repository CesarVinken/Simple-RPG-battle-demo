using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroSelectionCanvasController : MonoBehaviour
{
    public static HeroSelectionCanvasController Instance;

    [SerializeField] private Transform _heroTileContainer;
    [SerializeField] private ToBattleButton _toBattleButton;

    public void Awake()
    {
        if (_heroTileContainer == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find _heroTileContainer");
        }
        if (_toBattleButton == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find _toBattleButton");
        }

        Instance = this;
    }

    private void Start()
    {
        // We came back from the battle scene
        if (GameManager.Instance.PreviousScene != SceneType.None)
        {
            Setup();
            Initialise();
        }
    }

    public void Setup()
    {
        _toBattleButton.Setup();
    }

    // We want initialisation to take place after we have loaded in game data
    public void Initialise()
    {
        CreateHeroTiles();

        _toBattleButton.Initialise();
    }

    private void CreateHeroTiles()
    {
        Dictionary<int, IHero> heroes = GameManager.Instance.GetHeroes();

        foreach (KeyValuePair<int, IHero> item in heroes)
        {
            HeroTileFactory.CreateHeroTile(_heroTileContainer, item.Value);
        }
    }  
}
