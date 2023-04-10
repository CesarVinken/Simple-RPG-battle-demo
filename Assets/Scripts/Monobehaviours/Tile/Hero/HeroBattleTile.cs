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
    [SerializeField] private Transform _damageValueEffectContainer;
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _nameText;

    [SerializeField] private Healthbar _healthbar;

    [SerializeField] private Sprite _avatar;
    private float tapTimer = 0f;
    private Coroutine tapCoroutine = null;

    public IHero Hero { get; private set; }

    public void Setup(IHero hero)
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
        if (_damageValueEffectContainer == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Cannot find _damageValueEffectContainer");
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

        _healthbar.Setup();

        GameEventHandler gameEventHandler = ServiceLocator.Instance.Get<GameEventHandler>();
        gameEventHandler.HasTakenDamageEvent += OnHasTakenDamageEvent;
        gameEventHandler.HeroDefeatedEvent += OnHeroDefeatedEvent;
    }

    public void Initialise()
    {
        ServiceLocator.Instance.Get<ICanvasController>().RegisterTile(this);

        SetName();
        SetAvatar();

        _healthbar.Initialise(Hero);
    }

    public void Unload()
    {
        GameEventHandler gameEventHandler = ServiceLocator.Instance.Get<GameEventHandler>();
        gameEventHandler.HasTakenDamageEvent -= OnHasTakenDamageEvent;
        gameEventHandler.HeroDefeatedEvent -= OnHeroDefeatedEvent;
    }

    public IActor GetActor()
    {
        return Hero;
    }

    public Transform GetEffectContainer()
    {
        return _effectContainer;
    }

    public Transform GetDamageValueEffectContainer()
    {
        return _damageValueEffectContainer;
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
                // info panel should not work if the BattleEndPanel is open
                PanelHandler panelHandler = ServiceLocator.Instance.Get<PanelHandler>();
                List<IPanel> openPanels = panelHandler.GetOpenPanels();
                bool endGamePanelIsOpen = openPanels.OfType<IBattleEndPanel>().Any();

                if (!endGamePanelIsOpen)
                {
                    InfoPanelContainer panelContainer = ServiceLocator.Instance.Get<ICanvasController>().GetInfoPanelContainer();
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

            IAttack currentAttack = ServiceLocator.Instance.Get<AttackHandler>().GetCurrentAttack();
            if (currentAttack != null) return;

            ServiceLocator.Instance.Get<ICanvasController>().ActivateTile(this);
        }

        tapTimer = 0f;
    }

    public void OnHasTakenDamageEvent(object sender, HasTakenDamageEvent e)
    {
        if (!(e.HitActor is IHero)) return;

        if (e.HitActor.Id != Hero.Id) return;

        _healthbar.UpdateHealth(Hero);
    }

    public void OnHeroDefeatedEvent(object sender, HeroDefeatedEvent e)
    {
        if (e.Hero.Id != Hero.Id) return;

        BeDefeated();
    }

    #endregion
}
