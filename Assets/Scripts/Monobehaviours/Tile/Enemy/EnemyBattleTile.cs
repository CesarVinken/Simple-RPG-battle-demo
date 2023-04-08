using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyBattleTile : MonoBehaviour, IEnemyBattleTile
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _avatarImage;
    [SerializeField] private Transform _effectContainer;
    [SerializeField] private TextMeshProUGUI _nameText;

    [SerializeField] private Healthbar _healthbar;

    [SerializeField] private Sprite _avatarSprite;
    private ICanvasController _canvasController;

    public IEnemy Enemy { get; private set; }

    public void Setup(IEnemy enemy, ICanvasController canvasController)
    {
        if (_backgroundImage == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Cannot find _backgroundImage");
        }
        if (_avatarImage == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Cannot find _avatarImage");
        }
        if (_effectContainer == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Cannot find _effectContainer");
        }
        if (_nameText == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Cannot find name text");
        }

        if (_healthbar == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Cannot find _healthbar");
        }

        _canvasController = canvasController;
        Enemy = enemy;

        _healthbar.Setup();

        GameEventHandler.GetInstance().HasTakenDamageEvent += OnHasTakenDamageEvent;
        GameEventHandler.GetInstance().EnemyDefeatedEvent += OnEnemyDefeatedEvent;
    }

    public void Initialise()
    {
        SetName();
        SetAvatar();

        _canvasController.RegisterTile(this);
        _healthbar.Initialise(Enemy);
    }

    public void Unload()
    {
        GameEventHandler.GetInstance().HasTakenDamageEvent -= OnHasTakenDamageEvent;
        GameEventHandler.GetInstance().EnemyDefeatedEvent -= OnEnemyDefeatedEvent;
    }

    public IActor GetActor()
    {
        return Enemy;
    }

    public Transform GetEffectContainer()
    {
        return _effectContainer;
    }

    private void SetName()
    {
        _nameText.text = Enemy.Name;
    }

    private void BeDefeated()
    {
        _backgroundImage.color = ColourUtility.GetColour(ColourType.ErrorRed);
    }

    private async void SetAvatar()
    {
        // during the game we load the avatar for a hero only once
        if (Enemy.Avatar == null)
        {
            Sprite avatar = await EnemyTileFactory.LoadEnemyAvatar(Enemy);
            Enemy.SetAvatar(avatar);
            _avatarImage.sprite = avatar;
        }
        else
        {
            _avatarImage.sprite = Enemy.Avatar;
        }

        _avatarImage.enabled = true;
    }

    #region events

    public void OnHasTakenDamageEvent(object sender, HasTakenDamageEvent e)
    {
        if (e.HitActor is IHero) return;

        if (e.HitActor.Id != Enemy.Id) return;

        ConsoleLog.Log(LogCategory.General, $"Update health of enemy {Enemy.Name}");
        _healthbar.UpdateHealth(Enemy);
    }

    public void OnEnemyDefeatedEvent(object sender, EnemyDefeatedEvent e)
    {
        BeDefeated();
    }

    #endregion
}
