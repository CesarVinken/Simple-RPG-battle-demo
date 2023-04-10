using TMPro;
using UnityEngine;

public class DamageValueEffect : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private TextMeshProUGUI _text;

    private IActor _actor;

    public void Setup(IActor actor)
    {
        if (_animator == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find animator");
        }
        if (_text == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find _text");
        }

        _actor = actor;
    }

    public void Initialise()
    {
        _text.text = $"-{_actor.AttackPower}";
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
