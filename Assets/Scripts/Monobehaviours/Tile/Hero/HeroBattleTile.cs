using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroBattleTile : MonoBehaviour, IHeroBattleTile
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _avatarImage;
    [SerializeField] private Transform _effectContainer;
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _nameText;

    [SerializeField] private Healthbar _healthbar;

    [SerializeField] private Sprite _avatar;

    public IHero Hero { get; private set; }
    private ICanvasController _canvasController;

    public void Setup(IHero hero, ICanvasController canvasController)
    {
        if (_backgroundImage == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Cannot find _backgroundImage");
        }
        if (_avatarImage == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Cannot find image");
        }
        if (_effectContainer == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Cannot find _effectContainer");
        }
        if (_button == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Cannot find button");
        }
        if (_nameText == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Cannot find name text");
        }

        _canvasController = canvasController;
        Hero = hero;

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(delegate { OnClick(); });

        _healthbar.Setup();

        GameEventHandler.GetInstance().HasTakenDamageEvent += OnHasTakenDamageEvent;
        GameEventHandler.GetInstance().HeroDefeatedEvent += OnHeroDefeatedEvent;
    }

    public void Initialise()
    {
        _canvasController.RegisterTile(this);

        SetName();
        SetAvatar();

        _healthbar.Initialise(Hero);
    }

    public void Unload()
    {
        GameEventHandler.GetInstance().HasTakenDamageEvent -= OnHasTakenDamageEvent;
        GameEventHandler.GetInstance().HeroDefeatedEvent -= OnHeroDefeatedEvent;
    }

    public IActor GetActor()
    {
        return Hero;
    }

    public Transform GetEffectContainer()
    {
        return _effectContainer;
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

    private void BeDefeated()
    {
        _backgroundImage.color = ColourUtility.GetColour(ColourType.ErrorRed);
    }

    #region events

    public void OnClick()
    {
        if (Hero.CurrentHealth == 0) return;

        _canvasController.OnClickHero(this);
    }

    public void OnHasTakenDamageEvent(object sender, HasTakenDamageEvent e)
    {
        if (!(e.HitActor is IHero)) return;

        if (e.HitActor.Id != Hero.Id) return;

        ConsoleLog.Log(LogCategory.General, $"Update health for hero {Hero.Name} on {gameObject.name}");
        _healthbar.UpdateHealth(Hero);
    }

    public void OnHeroDefeatedEvent(object sender, HeroDefeatedEvent e)
    {
        if (e.Hero.Id != Hero.Id) return;

        BeDefeated();
    }

    #endregion
}
