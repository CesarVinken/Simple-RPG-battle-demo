using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HeroSelectionCanvasController : MonoBehaviour, ICanvasController
{
    public static HeroSelectionCanvasController Instance;

    [SerializeField] private Transform _tileRow1;
    [SerializeField] private Transform _tileRow2;
    [SerializeField] private VerticalLayoutGroup _heroTileContainerLayoutGroup;
    [SerializeField] private ToBattleButton _toBattleButton;
    [SerializeField] private InfoPanelContainer _infoPanelContainer;

    private List<ITile> _tiles = new List<ITile>();
    private List<IPanel> _openPanels = new List<IPanel>();

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
        ServiceLocator.Instance.Get<HeroSelectionHandler>();        
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

    public void Unload()
    {

    }

    public void ToBattle()
    {
        HeroSelectionHandler heroSelectionHandler = ServiceLocator.Instance.Get<HeroSelectionHandler>();
        _selectedHeroesAsset.selectedHeroes = heroSelectionHandler.GetSelectedHeros();
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

    public ToBattleButton GetToBattleButton()
    {
        return _toBattleButton;
    }

    public InfoPanelContainer GetInfoPanelContainer()
    {
        return _infoPanelContainer;
    }

    public void RegisterTile(ITile tile)
    {
        _tiles.Add(tile);

        if(_tiles.Count == GameManager.Instance.GetPlayerHeroes().Count)
        {
            StartCoroutine(UpdateCanvas());
        }
    }

    // There is a Unity bug that tends to mix up the width of the vertical layout group.
    // This function works around this by forcing a refresh of the width. That way the tiles are put in the right position
    private IEnumerator UpdateCanvas()
    {
        yield return null;
        _heroTileContainerLayoutGroup.enabled = false;
        yield return null;
        _heroTileContainerLayoutGroup.enabled = true;
        Canvas.ForceUpdateCanvases();
    }

    public void OnClickHero(IHeroTile tile)
    {
        if(!(tile is HeroSelectionTile))
        {
            ConsoleLog.Error(LogCategory.General, $"This tile is not a HeroSelectionTile");
        }

        HeroSelectionHandler heroSelectionHandler = ServiceLocator.Instance.Get<HeroSelectionHandler>();
        heroSelectionHandler.HandleTileSelection(tile as HeroSelectionTile);
    }
}
