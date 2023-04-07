using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private RectTransform _background;
    [SerializeField] private RectTransform _currentHealth;
    
    public void Setup()
    {
        if (_background == null) { ConsoleLog.Error(LogCategory.General, $"Cannot find background"); }
        if (_currentHealth == null) { ConsoleLog.Error(LogCategory.General, $"Cannot find _currentHealth"); }
    }

    public void Initialise(IActor actor)
    {
        UpdateHealth(actor);
    }

    public void UpdateHealth(IActor actor)
    {
        float healthPercentage = actor.CurrentHealth / actor.MaxHealth;

        ConsoleLog.Log(LogCategory.General, $"_background == null {_background == null}.");
        ConsoleLog.Log(LogCategory.General, $"gameObject {gameObject.name}");
        ConsoleLog.Log(LogCategory.General, $"actor {actor.Name}");
        
        ConsoleLog.Log(LogCategory.General, $"_background.rect == null {_background.rect == null}");
        float fullWidth = _background.rect.width;

        _currentHealth.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, fullWidth * healthPercentage);
    }
}
