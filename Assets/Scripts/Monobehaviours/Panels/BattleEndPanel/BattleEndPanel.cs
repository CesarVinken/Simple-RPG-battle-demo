using TMPro;
using UnityEngine;

public class BattleEndPanel : MonoBehaviour, IBattleEndPanel
{
    [SerializeField] private TextMeshProUGUI _headerLabel;
    [SerializeField] private ToHeroSelectionButton _toHeroSelectionButton;
    [SerializeField] private HeroBattleResultsContainer _heroBattleResultsContainer;

    public void Setup()
    {
        if (_toHeroSelectionButton == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find _toHeroSelectionButton");
        }
        if (_headerLabel == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find header label");
        }
        if (_heroBattleResultsContainer == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find _heroBattleResultsContainer");
        }

        _heroBattleResultsContainer.Setup();
        _toHeroSelectionButton.Setup();
    }

    public void Initialise(bool hasWon)
    {
        if (hasWon)
        {
            _headerLabel.text = "You won!";
        }
        else
        {
            _headerLabel.text = "You were defeated";
        }

        _heroBattleResultsContainer.Initialise();
        _toHeroSelectionButton.Initialise();
        Register();
    }

    public void Register()
    {
        ServiceLocator.Instance.Get<PanelHandler>().RegisterPanel(this);
    }

    public void Deregister()
    {
        ServiceLocator.Instance.Get<PanelHandler>().DeregisterPanel(this);
        Destroy(gameObject);
    }
}
