using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyTile : MonoBehaviour, IEnemyTile
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _avatarImage;
    [SerializeField] private TextMeshProUGUI _nameText;

    [SerializeField] private Healthbar _healthbar;

    [SerializeField] private Sprite _avatarSprite;
    private ICanvasController _canvasController;

    public IEnemy Enemy { get; private set; }

    public void Setup(IEnemy enemy)
    {
        if (_backgroundImage == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find _backgroundImage");
        }
        if (_avatarImage == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find _avatarImage");
        }
        if (_nameText == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find name text");
        }

        if (_healthbar == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find _healthbar");
        }

        _canvasController = BattleCanvasController.Instance;
        Enemy = enemy;

        _healthbar.Setup();

        GameEventHandler.GetInstance().HasTakenDamageEvent += OnHasTakenDamageEvent;
    }

    public void Initialise()
    {
        SetName();
        SetAvatar();

        _canvasController.RegisterTile(this);
        _healthbar.Initialise(Enemy);
    }
    public IActor GetActor()
    {
        return Enemy;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    private void SetName()
    {
        _nameText.text = Enemy.Name;
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

    public void OnHasTakenDamageEvent(object sender, HasTakenDamageEvent e)
    {
        if (e.HitActor is IHero) return;

        if (e.HitActor.Id != Enemy.Id) return;

        _healthbar.UpdateHealth(Enemy);
    }
}
