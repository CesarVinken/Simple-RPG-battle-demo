using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToBattleButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    private HeroSelectionCanvasController _canvasController;
    public bool IsEnabled { get; private set; } = false;

    public void Setup(HeroSelectionCanvasController canvasController)
    {
        if (_button == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could not find button on {gameObject.name}");
        }

        _canvasController = canvasController;

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(delegate { OnClick(); });
    }

    public void Initialise()
    {
        Disable();
    }

    private void OnClick()
    {
        _canvasController.ToScene(SceneType.Battle);
    }

    public void Enable()
    {
        IsEnabled = true;
        _button.interactable = true;
    }

    public void Disable()
    {
        IsEnabled = false;
        _button.interactable = false;
    }
}