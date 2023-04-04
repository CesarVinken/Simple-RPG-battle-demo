using System.Collections.Generic;
using UnityEngine;

public class HeroSelectionCanvasController : MonoBehaviour
{
    public static HeroSelectionCanvasController Instance;

    [SerializeField] private Transform _heroTileContainer;

    public void Awake()
    {
        if (_heroTileContainer == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find _heroTileContainer");
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
    }

    // We want initialisation to take place after we have loaded in game data
    public void Initialise()
    {
        CreateHeroTiles();
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
