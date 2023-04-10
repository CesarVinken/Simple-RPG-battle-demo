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
        float fullWidth = _background.rect.width;

        _currentHealth.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, fullWidth * healthPercentage);
    }
}
