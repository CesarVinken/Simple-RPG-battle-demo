using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleCanvasController : MonoBehaviour, ICanvasController
{
    public static BattleCanvasController Instance;

    [SerializeField] private ToHeroSelectionButton _toHeroSelectionButton;
    [SerializeField] private SpawnpointContainer _spawnpointContainer;

    private List<IHeroTile> _tiles = new List<IHeroTile>();
    [SerializeField] private SelectedHeroes _selectedHeroesAsset;


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

        EnemyBlueprint enemyBlueprint = new EnemyBlueprint();
        enemyBlueprint.Id = 0;
        enemyBlueprint.Name = "Aap";
        IEnemy enemyToSpawn = new Enemy(enemyBlueprint);

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

    public void AddTile(IHeroTile tile)
    {
        _tiles.Add(tile);
    }

    public void OnClickHero(IHeroTile tile)
    {
        ConsoleLog.Log(LogCategory.General, $"attack");
    }
}
