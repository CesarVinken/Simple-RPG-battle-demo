using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroSelectionTile : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _nameText;

    [SerializeField] private Sprite _avatar;
    private IHero _hero;

    public void Setup(IHero hero)
    {
        if(_image == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find image");
        }
        if(_nameText == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find name text");
        }

        _hero = hero;
    }

    public void Initialise()
    {
        _nameText.text = _hero.Name;
    }
}
