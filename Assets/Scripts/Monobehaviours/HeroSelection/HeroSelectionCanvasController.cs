using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HeroSelectionCanvasController : MonoBehaviour, ICanvasController
{
    [SerializeField] private Transform _tileRow1;
    [SerializeField] private Transform _tileRow2;
    [SerializeField] private VerticalLayoutGroup _heroTileContainerLayoutGroup;
    [SerializeField] private ToBattleButton _toBattleButton;
    [SerializeField] private InfoPanelContainer _infoPanelContainer;

    private Dictionary<IActor, ITile> _tilesByActor = new Dictionary<IActor, ITile>();

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
    }

    private void Start()
    {
        ServiceLocator.Instance.Register<ICanvasController>(this);

        Setup();
        Initialise();
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
        ServiceLocator.Instance.Deregister<ICanvasController>();
    }

    public void ToScene(SceneType sceneType)
    {
        Unload();
        ServiceLocator.Instance.Get<SceneChangeHandler>().ChangeScene(sceneType);
    }

    private void CreateHeroTiles()
    {
        DataHandler dataHandler = ServiceLocator.Instance.Get<DataHandler>();
        List<IHero> heroes = dataHandler.PlayerData.Heroes.Values.ToList();

        for (int i = 0; i < heroes.Count; i++)
        {
            Transform container = i < 5 ? _tileRow1 : _tileRow2;
            HeroTileFactory.CreateHeroSelectionTile(container, heroes[i]);
        }
    }

    public InfoPanelContainer GetInfoPanelContainer()
    {
        return _infoPanelContainer;
    }

    public void RegisterTile(ITile tile)
    {
        _tilesByActor.Add(tile.GetActor(), tile);
    }

    public ITile GetTile(IActor actor)
    {
        if (_tilesByActor.TryGetValue(actor, out ITile tile))
        {
            return tile;
        }

        ConsoleLog.Error(LogCategory.General, $"Could not find a tile for {actor.Name}");
        return null;
    }

    public void ActivateTile(ITile tile)
    {
        if(!(tile is HeroSelectionTile))
        {
            ConsoleLog.Error(LogCategory.General, $"This tile is not a HeroSelectionTile");
        }

        HeroSelectionHandler heroSelectionHandler = ServiceLocator.Instance.Get<HeroSelectionHandler>();
        heroSelectionHandler.HandleTileSelection(tile as HeroSelectionTile);
        heroSelectionHandler.HandleToBattleButton(_toBattleButton);
    }
}
