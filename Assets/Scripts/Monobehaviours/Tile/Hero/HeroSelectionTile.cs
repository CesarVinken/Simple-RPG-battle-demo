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

    public IHero Hero { get; private set; }
    
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

        Hero = hero;
        _canvasController = HeroSelectionCanvasController.Instance;
        _selectionBorderImage.enabled = false;

        SetTileSize();

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(delegate { OnClick(); });
    }

    public void Initialise()
    {       
        _canvasController.RegisterTile(this);

        SetName();
        SetAvatar();
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

    private void SetTileSize()
    {
        RectTransform parentRect = HeroSelectionCanvasController.Instance.GetComponent<RectTransform>();
        RectTransform rectTransform = GetComponent<RectTransform>();

        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, parentRect.sizeDelta.x / 5f);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, parentRect.sizeDelta.x / 5f);
    }

    private async void SetAvatar()
    {
        // during the game we load the avatar for a hero only once
        if(Hero.Avatar == null)
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
