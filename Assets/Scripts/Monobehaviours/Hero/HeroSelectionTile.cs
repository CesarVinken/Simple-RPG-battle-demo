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
        SetName();
        SetAvatar();
    }

    private void SetName()
    {
        _nameText.text = _hero.Name;
    }

    private async void SetAvatar()
    {
        // during the game we load the avatar for a hero only once
        if(_hero.Avatar == null)
        {
            Sprite avatar = await HeroTileFactory.LoadHeroAvatar(_hero);
            _hero.SetAvatar(avatar);
            _image.sprite = avatar;
        }
        else
        {
            _image.sprite = _hero.Avatar;
        }
    }
}
