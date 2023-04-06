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

    private IHero _hero;
    private BattleCanvasController _canvasController;

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
        _hero = hero;

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(delegate { OnClick(); });

        _healthbar.Setup();
    }

    public void Initialise()
    {
        _canvasController.AddTile(this);

        SetName();
        SetAvatar();

        _healthbar.Initialise(_hero);
    }

    private void SetName()
    {
        _nameText.text = _hero.Name;
    }

    private async void SetAvatar()
    {
        // during the game we load the avatar for a hero only once
        if (_hero.Avatar == null)
        {
            Sprite avatar = await HeroTileFactory.LoadHeroAvatar(_hero);
            _hero.SetAvatar(avatar);
            _avatarImage.sprite = avatar;
        }
        else
        {
            _avatarImage.sprite = _hero.Avatar;
        }

        _avatarImage.enabled = true;
    }

    public void OnClick()
    {
        _canvasController.OnClickHero(this);
    }
}
