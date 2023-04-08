using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroBattleTile : MonoBehaviour, IHeroBattleTile, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _avatarImage;
    [SerializeField] private Transform _effectContainer;
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _nameText;

    [SerializeField] private Healthbar _healthbar;

    [SerializeField] private Sprite _avatar;
    private float tapTimer = 0f; //TODO check if we can do something with this shared tile code
    private Coroutine tapCoroutine = null;

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

    private IEnumerator TapTimer()
    {
        while (true)
        {
            yield return null;
            tapTimer += Time.deltaTime;

            if (tapTimer >= 3f)
            {
                Debug.Log("Long tap detected");

                //TODO Use service locator
                // info panel should not work if the BattleEndPanel is open
                List<IPanel> openPanels = _canvasController.PanelHandler.GetOpenPanels();
                bool endGamePanelIsOpen = openPanels.OfType<IBattleEndPanel>().Any();

                if (!endGamePanelIsOpen)
                {
                    InfoPanelContainer panelContainer = _canvasController.GetInfoPanelContainer();
                    UIPanelFactory.CreateHeroInfoPanel(panelContainer, Hero);
                }
                
                StopCoroutine(tapCoroutine);
            }
        }
    }

    #region events

    public void OnPointerDown(PointerEventData eventData)
    {
        tapCoroutine = StartCoroutine(TapTimer());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopCoroutine(tapCoroutine);

        if (tapTimer < 3f)
        {
            if (Hero.CurrentHealth == 0) return;

            _canvasController.OnClickHero(this);
        }

        tapTimer = 0f;
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
