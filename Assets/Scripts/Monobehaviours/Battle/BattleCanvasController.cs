using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BattleCanvasController : MonoBehaviour, ICanvasController
{
    [SerializeField] private SpawnpointContainer _spawnpointContainer;

    //[SerializeField] private SelectedHeroes _selectedHeroesAsset;
    [SerializeField] private InfoPanelContainer _infoPanelContainer;

    private Dictionary<IActor, ITile> _tilesByActor = new Dictionary<IActor, ITile>();

    public void Awake()
    {

    }

    private void Start()
    {
        ServiceLocator.Instance.Register<ICanvasController>(this);
        Setup();
        Initialise();
    }

    public void Setup()
    {
        if (_spawnpointContainer == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find _spawnpointContainer");
        }

        _spawnpointContainer.Setup();

        GameEventHandler gameEventHandler = ServiceLocator.Instance.Get<GameEventHandler>();
        gameEventHandler.HeroDefeatedEvent += OnHeroDefeatedEvent;
        gameEventHandler.EnemyDefeatedEvent += OnEnemyDefeatedEvent;
        gameEventHandler.HasTakenDamageEvent += OnHasTakenDamageEvent;
    }

    // We want initialisation to take place after we have loaded in game data
    public async void Initialise()
    {
        List<IHero> heroesToSpawn = await ServiceLocator.Instance.Get<ScriptableObjectHandler>().GetSelectedHeroes();
        IEnemy enemyToSpawn = EnemyFactory.CreateRandomEnemy();

        _spawnpointContainer.Initialise(heroesToSpawn, enemyToSpawn);
    }

    public void Unload()
    {
        ServiceLocator.Instance.Deregister<ICanvasController>();

        GameEventHandler gameEventHandler = ServiceLocator.Instance.Get<GameEventHandler>();
        gameEventHandler.HeroDefeatedEvent -= OnHeroDefeatedEvent;
        gameEventHandler.EnemyDefeatedEvent -= OnEnemyDefeatedEvent;

        foreach (KeyValuePair<IActor, ITile> item in _tilesByActor)
        {
            item.Value.Unload();
        }
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

    public void ToScene(SceneType sceneType)
    {
        Unload();
        GameManager.Instance.ToScene(sceneType);
    }

    public InfoPanelContainer GetInfoPanelContainer()
    {
        return _infoPanelContainer;
    }

    public void ActivateTile(ITile tile)
    {
        IAttack currentAttack = ServiceLocator.Instance.Get<AttackHandler>().GetCurrentAttack();

        if (currentAttack != null) return;

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
        AttackHandler attackHandler = ServiceLocator.Instance.Get<AttackHandler>();
        attackHandler.SetAttack(attack);
        attackHandler.SetTarget(possibleTargets);
        attackHandler.ExecutePhase(AttackPhase.Attacking);
    }

    #region events

    public void OnEnemyDefeatedEvent(object sender, EnemyDefeatedEvent e)
    {
        PanelHandler panelHandler = ServiceLocator.Instance.Get<PanelHandler>();
        List<IPanel> openPanels = panelHandler.GetOpenPanels();

        for (int i = 0; i < openPanels.Count; i++)
        {
            openPanels[i].Deregister();
        }

        UIPanelFactory.CreateBattleEndPanel(transform, true);
    }

    public void OnHasTakenDamageEvent(object sender, HasTakenDamageEvent e)
    {
        if (e.HitActor is Enemy) return;

        ServiceLocator.Instance.Get<AttackHandler>().SetAttack(null);
    }

    public void OnHeroDefeatedEvent(object sender, HeroDefeatedEvent e)
    {
        PanelHandler panelHandler = ServiceLocator.Instance.Get<PanelHandler>();
        List<IActor> aliveHeroes = _tilesByActor.Keys.Where(t => t is IHero && t.CurrentHealth > 0).ToList();
        if(aliveHeroes.Count == 0)
        {
            List<IPanel> openPanels = panelHandler.GetOpenPanels();

            for (int i = 0; i < openPanels.Count; i++)
            {
                openPanels[i].Deregister();
            }

            UIPanelFactory.CreateBattleEndPanel(transform, false);
        }
    }

    #endregion
}

