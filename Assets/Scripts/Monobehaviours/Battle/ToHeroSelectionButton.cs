using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToHeroSelectionButton : MonoBehaviour
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
            
    }

    public void OnClick()
    {
        //TODO Use service locator:
        BattleCanvasController.Instance.Unload();
        GameManager.Instance.ToScene(SceneType.HeroSelection);  
    }
}