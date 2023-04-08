using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroBattleResults : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _avatarImage;

    [SerializeField] private TextMeshProUGUI _nameLabel;
    [SerializeField] private TextMeshProUGUI _experienceLabel;
    [SerializeField] private TextMeshProUGUI _levelLabel;
    [SerializeField] private TextMeshProUGUI _healthLabel;
    [SerializeField] private TextMeshProUGUI _attackPowerLabel;

    private IHero _hero;

    public void Setup(IHero hero)
    {
        if (_backgroundImage == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could not _backgroundImage");
        }
        if (_avatarImage == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could not _avatarImage");
        }

        if (_nameLabel == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could not _nameLabel");
        }
        if(_experienceLabel == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could not _experienceLabel");
        }
        if(_levelLabel == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could not _levelLabel");
        }
        if(_healthLabel == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could not _healthLabel");
        }
        if(_attackPowerLabel == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could not _attackPowerLabel");
        }

        _hero = hero;
    }

    public void Initialise()
    {
        gameObject.name = $"HeroBattleResults - {_hero.Name}";

        AttributeProjection attributeProjection = new AttributeProjection(_hero);

        _nameLabel.text = _hero.Name;

        SetExperience(attributeProjection);
        SetLevel(attributeProjection);
        SetMaxHealth(attributeProjection);
        SetAttackPower(attributeProjection);

        SetAvatar();
        SetBackground();
    }

    private void SetExperience(AttributeProjection attributeProjection)
    {
        float difference = attributeProjection.Experience - _hero.Experience;
        if (difference == 0)
        {
            _experienceLabel.text = $"Experience: {attributeProjection.Experience}";
        }
        else
        {
            _experienceLabel.text = $"Experience: {attributeProjection.Experience} (+{difference})";
        }
    }

    private void SetLevel(AttributeProjection attributeProjection)
    {
        float difference = attributeProjection.Level - _hero.Level;
        if (difference == 0)
        {
            _levelLabel.text = $"Level: {attributeProjection.Level}";
        }
        else
        {
            _levelLabel.text = $"Level: {attributeProjection.Level} (+{difference})";
        }
    }

    private void SetMaxHealth(AttributeProjection attributeProjection)
    {
        float difference = attributeProjection.MaxHealth - _hero.MaxHealth;
        if (difference == 0)
        {
            _healthLabel.text = $"Health: {attributeProjection.MaxHealth}";
        }
        else
        {
            _healthLabel.text = $"Health: {attributeProjection.MaxHealth} (+{difference})";
        }
    }

    private void SetAttackPower(AttributeProjection attributeProjection)
    {
        float difference = attributeProjection.AttackPower - _hero.AttackPower;
        if (difference == 0)
        {
            _attackPowerLabel.text = $"Attack Power: {attributeProjection.AttackPower}";
        }
        else
        {
            _attackPowerLabel.text = $"Attack Power: {attributeProjection.AttackPower} (+{difference})";
        }
    }

    private async void SetAvatar()
    {
        // during the game we load the avatar for a hero only once
        if (_hero.Avatar == null)
        {
            Sprite avatar = await HeroTileFactory.LoadHeroAvatar(_hero);
            _hero.SetAvatar(avatar);
            _avatarImage.sprite = avatar;
        }
        else
        {
            _avatarImage.sprite = _hero.Avatar;
        }

        _avatarImage.enabled = true;
    }

    private void SetBackground()
    {
        if(_hero.CurrentHealth == 0)
        {
            _backgroundImage.color = ColourUtility.GetColour(ColourType.ErrorRed);
        }
        else
        {
            _backgroundImage.color = ColourUtility.GetColour(ColourType.Blue);
        }
    }

    public struct AttributeProjection
    {
        public int Level;
        public int Experience;
        public float MaxHealth;
        public float AttackPower;

        public AttributeProjection(IHero hero)
        {
            if(hero.CurrentHealth == 0)
            {
                Level = hero.Level;
                Experience = hero.Experience;
                MaxHealth = hero.MaxHealth;
                AttackPower = hero.AttackPower;
            }

            else
            {
                Experience = hero.Experience + 1;
                if(Experience % 5 != 0)
                {
                    Level = hero.Level;
                    MaxHealth = hero.MaxHealth;
                    AttackPower = hero.AttackPower;
                }
                else
                {
                    Level = hero.Level + 1;
                    MaxHealth = hero.MaxHealth * 1.1f;
                    AttackPower = hero.AttackPower * 1.1f;
                }
            }
        }
    }
}
