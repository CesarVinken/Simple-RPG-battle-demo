using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroSelectionTile : MonoBehaviour, IHeroTile, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _selectionBorderImage;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _avatarImage;
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _nameText;

    [SerializeField] private Sprite _avatarSprite;
    private float tapTimer = 0f;
    private Coroutine tapCoroutine = null;

    public IHero Hero { get; private set; }
    
    public void Setup(IHero hero)
    {
        if (_selectionBorderImage == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Cannot find _selectionBorderImage");
        }
        if (_backgroundImage == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Cannot find _backgroundImage");
        }
        if (_avatarImage == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Cannot find _avatarImage");
        }
        if (_button == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Cannot find button");
        }
        if (_nameText == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Cannot find name text");
        }

        Hero = hero;
        Hero.ResetHealth();

        _selectionBorderImage.enabled = false;

        SetTileSize();  
    }

    public void Initialise()
    {
        ServiceLocator.Instance.Get<ICanvasController>().RegisterTile(this);

        SetName();
        SetAvatar();
    }

    public void Unload()
    {
        StopCoroutine(tapCoroutine);
    }

    public IActor GetActor()
    {
        return Hero;
    }

    public Transform GetEffectContainer()
    {
        return transform;
    }

    private void SetName()
    {
        _nameText.text = Hero.Name;
    }

    private void SetTileSize()
    {
        Transform containerTransform = transform.parent.parent;
        float containerWidth = containerTransform.GetComponent<RectTransform>().rect.width;
        RectTransform rectTransform = GetComponent<RectTransform>();

        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, containerWidth / 5f);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, containerWidth / 5f);
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

    public void OnPointerDown(PointerEventData eventData)
    {
        tapCoroutine = StartCoroutine(TapTimer());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopCoroutine(tapCoroutine);

        if (tapTimer < 3f)
        {
            ServiceLocator.Instance.Get<ICanvasController>().ActivateTile(this);
        }

        tapTimer = 0f;
    }

    private IEnumerator TapTimer()
    {
        while (true)
        {
            yield return null;
            tapTimer += Time.deltaTime;

            if (tapTimer >= 3f)
            {
                InfoPanelContainer infoPanelContainer = ServiceLocator.Instance.Get<ICanvasController>().GetInfoPanelContainer();
                UIPanelFactory.CreateHeroInfoPanel(infoPanelContainer, Hero);

                StopCoroutine(tapCoroutine);
            }
        }
    }

    public void Select()
    {
        HeroSelectionHandler heroSelectionHandler = ServiceLocator.Instance.Get<HeroSelectionHandler>();
        _selectionBorderImage.enabled = true;
        heroSelectionHandler.AddToSelectedTiles(this);
    }

    public void Deselect()
    {
        HeroSelectionHandler heroSelectionHandler = ServiceLocator.Instance.Get<HeroSelectionHandler>();
        _selectionBorderImage.enabled = false;
        heroSelectionHandler.RemoveFromSelectedTiles(this);
    }
}
