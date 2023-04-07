using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleCanvasController : MonoBehaviour, ICanvasController
{
    public static BattleCanvasController Instance;

    [SerializeField] private ToHeroSelectionButton _toHeroSelectionButton;
    [SerializeField] private SpawnpointContainer _spawnpointContainer;

    [SerializeField] private SelectedHeroes _selectedHeroesAsset;
    public BattleHandler BattleHandler { get; private set; }
    private Dictionary<IActor, ITile> _tilesByActor = new Dictionary<IActor, ITile>();

    public void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // We came back from the hero selection scene
        if (GameManager.Instance.PreviousScene != SceneType.None)
        {
            Setup();
            Initialise();
        }
    }

    public void Setup()
    {
        if (_toHeroSelectionButton == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find _toHeroSelectionButton");
        }
        if (_spawnpointContainer == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find _spawnpointContainer");
        }

        _toHeroSelectionButton.Setup();
        _spawnpointContainer.Setup();

    }

    // We want initialisation to take place after we have loaded in game data
    public void Initialise()
    {
        List<IHero> heroesToSpawn = GetSelectedHeroes();
        ConsoleLog.Warning(LogCategory.General, $"number of selected heroes: {_selectedHeroesAsset.selectedHeroes.Count}");

        IEnemy enemyToSpawn = EnemyFactory.CreateRandomEnemy();

        _toHeroSelectionButton.Initialise();
        _spawnpointContainer.Initialise(heroesToSpawn, enemyToSpawn);
    }

    private List<IHero> GetSelectedHeroes()
    {
        if(GameManager.Instance.PreviousScene == SceneType.None) // we started the battle scene directle from Unity inspector
        {
            List<IHero> playerHeroes = GameManager.Instance.GetPlayerHeroes().Values.ToList();

            if (playerHeroes.Count < 3)
            {
                ConsoleLog.Error(LogCategory.General, $"The player needs at least 3 heroes but currently has only {playerHeroes.Count}");
            }

            List<IHero> selectedHeroes = new List<IHero>();

            for (int i = 0; i < 3; i++)
            {
                selectedHeroes.Add(playerHeroes[i]);
            }

            _selectedHeroesAsset.selectedHeroes = selectedHeroes;
        }

        return _selectedHeroesAsset.selectedHeroes;
    }

    public void RegisterTile(ITile tile)
    {
        _tilesByActor.Add(tile.GetActor(), tile);
    }

    public void OnClickHero(IHeroTile tile)
    {
        // For now we only have default attacks in our game.
        IAttack attack = AttackFactory.CreateAttack<DefaultAttack>(tile.Hero);
        BattleHandler = new BattleHandler(_tilesByActor.Values.ToList());
        BattleHandler.Attack(attack);
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
}
