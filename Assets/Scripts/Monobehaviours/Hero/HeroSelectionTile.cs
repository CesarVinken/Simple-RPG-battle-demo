using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroSelectionTile : MonoBehaviour, IHeroTile
{
    [SerializeField] private Image _selectionBorderImage;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _avatarImage;
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _nameText;

    [SerializeField] private Sprite _avatarSprite;
    private IHero _hero;
    private ICanvasController _canvasController;

    public void Setup(IHero hero)
    {
        if (_selectionBorderImage == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find _selectionBorderImage");
        }
        if (_backgroundImage == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find _backgroundImage");
        }
        if (_avatarImage == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find _avatarImage");
        }
        if (_button == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find button");
        }
        if (_nameText == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find name text");
        }

        _hero = hero;
        _canvasController = HeroSelectionCanvasController.Instance;

        _selectionBorderImage.enabled = false;

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(delegate { OnClick(); });
    }

    public void Initialise()
    {       
        _canvasController.AddTile(this);

        SetName();
        SetAvatar();
    }

    private void SetName()
    {
        _nameText.text = _hero.Name;
    }

    private async void SetAvatar()
    {
        // during the game we load the avatar for a hero only once
        if(_hero.Avatar == null)
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

    public void Select(HeroSelectionHandler heroSelectionHandler)
    {
        _selectionBorderImage.enabled = true;
        heroSelectionHandler.AddToSelectedTiles(this);
    }

    public void Deselect(HeroSelectionHandler heroSelectionHandler)
    {
        _selectionBorderImage.enabled = false;
        heroSelectionHandler.RemoveFromSelectedTiles(this);
    }
}
