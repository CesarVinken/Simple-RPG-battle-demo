using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroInfoPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameLabel;
    [SerializeField] private TextMeshProUGUI _levelLabel;
    [SerializeField] private TextMeshProUGUI _experienceLabel;
    [SerializeField] private TextMeshProUGUI _maxHealthLabel;
    [SerializeField] private TextMeshProUGUI _attackPowerLabel;

    [SerializeField] RectTransform _rectTransform;
    [SerializeField] private LayoutElement _layoutElement;

    private List<TextMeshProUGUI> _textMeshes = new List<TextMeshProUGUI>();

    private const int _characterWrapLimit = 60;

    public void Setup()
    {
        _textMeshes.Add(_nameLabel);
        _textMeshes.Add(_levelLabel);
        _textMeshes.Add(_experienceLabel);
        _textMeshes.Add(_maxHealthLabel);
        _textMeshes.Add(_attackPowerLabel);

        for (int i = 0; i < _textMeshes.Count; i++)
        {
            if(_textMeshes[i] == null)
            {
                ConsoleLog.Error(LogCategory.General, $"Cannot find {_textMeshes[i]}");

            }
        }

        if (_rectTransform == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find _rectTransform");
        }
        if (_layoutElement == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find _layoutElement");
        }
    }

    public void Initialise(IHero hero)
    {
        SetTextLabels(hero);
        SetPosition();
    }

    private void SetTextLabels(IHero hero)
    {
        _nameLabel.text = $"Name: {hero.Name}";
        _levelLabel.text = $"Level: {hero.Level}";
        _experienceLabel.text = $"Experience: {hero.Experience}";
        _maxHealthLabel.text = $"Max Health: {hero.MaxHealth}";
        _attackPowerLabel.text = $"Attack Power: {hero.AttackPower}";
    }

    private void SetPosition()
    {
        bool surpassesCharacterWrapLimit = false;

        for (int i = 0; i < _textMeshes.Count; i++)
        {
            int textLength = _textMeshes[i].text.Length;

            if (textLength > _characterWrapLimit)
            {
                surpassesCharacterWrapLimit = true;
            }
        }

        _layoutElement.enabled = surpassesCharacterWrapLimit;

        Vector2 mousePosition = Input.mousePosition;

        float pivotX = mousePosition.x / Screen.width;
        float pivotY = mousePosition.y / Screen.height;
        _rectTransform.pivot = new Vector2(pivotX, pivotY);

        transform.position = mousePosition;
    }
}
