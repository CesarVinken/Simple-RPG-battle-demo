using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroSelectionCanvasController : MonoBehaviour, ICanvasController
{
    public static HeroSelectionCanvasController Instance;

    [SerializeField] private Transform _tileRow1;
    [SerializeField] private Transform _tileRow2;
    [SerializeField] private ToBattleButton _toBattleButton;

    private List<IHeroTile> _tiles = new List<IHeroTile>();
    private HeroSelectionHandler _heroSelectionHandler;

    public SelectedHeroes _selectedHeroesAsset;

    public void Awake()
    {
        if (_tileRow1 == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find _tileRow1");
        }
        if (_tileRow2 == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find _tileRow2");
        }
        if (_toBattleButton == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find _toBattleButton");
        }

        Instance = this;
        _heroSelectionHandler = new HeroSelectionHandler(_toBattleButton);
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
        _toBattleButton.Setup(this);
    }

    // We want initialisation to take place after we have loaded in game data
    public void Initialise()
    {
        CreateHeroTiles();

        _toBattleButton.Initialise();
    }

    public void ToBattle()
    {
        _selectedHeroesAsset.selectedHeroes = _heroSelectionHandler.GetSelectedHeros();
        GameManager.Instance.ToScene(SceneType.Battle);
    }

    private void CreateHeroTiles()
    {
        List<IHero> heroes = GameManager.Instance.GetPlayerHeroes().Values.ToList();

        for (int i = 0; i < heroes.Count; i++)
        {
            Transform container = i < 5 ? _tileRow1 : _tileRow2;
            HeroTileFactory.CreateHeroSelectionTile(container, heroes[i]);
        }
    }

    public void AddTile(IHeroTile tile)
    {
        _tiles.Add(tile);
    }

    public void OnClickHero(IHeroTile tile)
    {
        if(!(tile is HeroSelectionTile))
        {
            ConsoleLog.Error(LogCategory.General, $"This tile is not a HeroSelectionTile");
        }

        _heroSelectionHandler.HandleTileSelection(tile as HeroSelectionTile);
    }
}
