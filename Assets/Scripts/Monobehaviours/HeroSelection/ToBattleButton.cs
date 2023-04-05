using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToBattleButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    public void Setup()
    {
        if (_button == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could not find button on {gameObject.name}");
        }

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(delegate { OnClick(); });
    }

    public void Initialise()
    {
        Disable();
    }

    private void OnClick()
    {
        GameManager.Instance.ToScene(SceneType.Battle);
    }

    public void Enable()
    {
        _button.interactable = true;
    }

    public void Disable()
    {
        _button.interactable = false;
    }
}