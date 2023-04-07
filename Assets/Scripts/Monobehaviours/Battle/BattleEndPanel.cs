using TMPro;
using UnityEngine;

public class BattleEndPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _headerLabel;
    [SerializeField] private ToHeroSelectionButton _toHeroSelectionButton;

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

        _toHeroSelectionButton.Initialise();
    }
}
