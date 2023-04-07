using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroBattleTile : MonoBehaviour, IHeroTile
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _avatarImage;
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _nameText;

    [SerializeField] private Healthbar _healthbar;

    [SerializeField] private Sprite _avatar;

    public IHero Hero { get; private set; }
    private ICanvasController _canvasController;

    public void Setup(IHero hero)
    {
        if (_backgroundImage == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find _backgroundImage");
        }
        if (_avatarImage == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find image");
        }
        if (_button == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find button");
        }
        if (_nameText == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find name text");
        }

        _canvasController = BattleCanvasController.Instance;
        Hero = hero;

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(delegate { OnClick(); });

        _healthbar.Setup();

        GameEventHandler.GetInstance().HasTakenDamageEvent += OnHasTakenDamageEvent;
    }

    public void Initialise()
    {
        _canvasController.RegisterTile(this);

        SetName();
        SetAvatar();

        _healthbar.Initialise(Hero);
    }

    public IActor GetActor()
    {
        return Hero;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    private void SetName()
    {
        _nameText.text = Hero.Name;
    }

    private async void SetAvatar()
    {
        // during the game we load the avatar for a hero only once
        if (Hero.Avatar == null)
        {
            Sprite avatar = await HeroTileFactory.LoadHeroAvatar(Hero);
            Hero.SetAvatar(avatar);
            _avatarImage.sprite = avatar;
        }
        else
        {
            _avatarImage.sprite = Hero.Avatar;
        }

        _avatarImage.enabled = true;
    }

    public void OnClick()
    {
        _canvasController.OnClickHero(this);
    }

    public void OnHasTakenDamageEvent(object sender, HasTakenDamageEvent e)
    {
        if (e.HitActor is IEnemy) return;

        if (e.HitActor.Id != Hero.Id) return;

        _healthbar.UpdateHealth(Hero);
    }
}
