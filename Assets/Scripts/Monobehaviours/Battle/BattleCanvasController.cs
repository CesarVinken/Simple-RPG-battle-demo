using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleCanvasController : MonoBehaviour, ICanvasController
{
    public static BattleCanvasController Instance;

    [SerializeField] private SpawnpointContainer _spawnpointContainer;

    [SerializeField] private SelectedHeroes _selectedHeroesAsset;
    public AttackHandler CurrentAttackHandler { get; private set; }
    private Dictionary<IActor, ITile> _tilesByActor = new Dictionary<IActor, ITile>();
    //public bool InAttackRoutine { get; private set; }

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

        if (_spawnpointContainer == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find _spawnpointContainer");
        }

        _spawnpointContainer.Setup();

        GameEventHandler.GetInstance().HeroDefeatedEvent += OnHeroDefeatedEvent;
        GameEventHandler.GetInstance().EnemyDefeatedEvent += OnEnemyDefeatedEvent;
        GameEventHandler.GetInstance().HasTakenDamageEvent += OnHasTakenDamageEvent;
    }

    // We want initialisation to take place after we have loaded in game data
    public void Initialise()
    {
        List<IHero> heroesToSpawn = GetSelectedHeroes();
        ConsoleLog.Warning(LogCategory.General, $"number of selected heroes: {_selectedHeroesAsset.selectedHeroes.Count}");

        IEnemy enemyToSpawn = EnemyFactory.CreateRandomEnemy();

        _spawnpointContainer.Initialise(heroesToSpawn, enemyToSpawn);
    }

    public void Unload()
    {
        GameEventHandler.GetInstance().HeroDefeatedEvent -= OnHeroDefeatedEvent;
        GameEventHandler.GetInstance().EnemyDefeatedEvent -= OnEnemyDefeatedEvent;

        foreach (KeyValuePair<IActor, ITile> item in _tilesByActor)
        {
            item.Value.Unload();
        }
    }

    private List<IHero> GetSelectedHeroes()
    {
        if(GameManager.Instance.PreviousScene == SceneType.None) // we started the battle scene directly from Unity inspector
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
        if (CurrentAttackHandler != null) return;

        // For now we only have default attacks in our game.
        TriggerAttack(tile);
    
    }

    public void TriggerAttack(ITile attackingTile)
    {
        IActor attacker = attackingTile.GetActor();
        List<ITile> possibleTargets = new List<ITile>();
        if(attacker is IEnemy)
        {
            possibleTargets = _tilesByActor.Values.Where(v => v.GetActor() is IHero).ToList();
        }
        else
        {
            possibleTargets = _tilesByActor.Values.Where(v => v.GetActor() is IEnemy).ToList();
        }

        IAttack attack = AttackFactory.CreateAttack<DefaultAttack>(attackingTile.GetActor());
        CurrentAttackHandler = new AttackHandler(attack); // TODO use service locator 
        CurrentAttackHandler.AddTarget(possibleTargets);
        CurrentAttackHandler.ExecutePhase(AttackPhase.Attacking);
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

    #region events

    public void OnEnemyDefeatedEvent(object sender, EnemyDefeatedEvent e)
    {
        UIPanelFactory.CreateBattleEndPanel(transform, true);
    }

    public void OnHasTakenDamageEvent(object sender, HasTakenDamageEvent e)
    {
        if (e.HitActor is Enemy) return;

        CurrentAttackHandler = null;
    }

    public void OnHeroDefeatedEvent(object sender, HeroDefeatedEvent e)
    {
        List<IActor> aliveHeroes = _tilesByActor.Keys.Where(t => t is IHero && t.CurrentHealth > 0).ToList();
        if(aliveHeroes.Count == 0)
        {
            UIPanelFactory.CreateBattleEndPanel(transform, false);
        }
    }

    #endregion
}

