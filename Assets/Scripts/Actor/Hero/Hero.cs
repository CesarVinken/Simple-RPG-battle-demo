using System;
using UnityEngine;

public class Hero : IHero
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }
    public float AttackPower { get; private set; }
    public int XP { get; private set; }
    public int Level { get; private set; }
    public Sprite Avatar { get; private set; }

    private const float _baseHealth = 100;
    private const float _baseAttackPower = 10;
    private const int _levelThreshold = 2;

    public Hero(HeroBlueprint heroBlueprint)
    {
        Id = heroBlueprint.Id;
        Name = heroBlueprint.Name;

        XP = 0;
        Level = 1;
        MaxHealth = 100;
        CurrentHealth = MaxHealth;
        AttackPower = 10;
    }

    public void UpdateStats(int xp)
    {
        XP = xp;

        Level = 1 + Mathf.FloorToInt(XP / _levelThreshold);

        MaxHealth = _baseHealth * (float)Math.Pow((double)1.1f, (double)Level - 1);
        AttackPower = _baseAttackPower * (float)Math.Pow((double)1.1f, (double)Level - 1);
        CurrentHealth = MaxHealth;
        ConsoleLog.Log(LogCategory.General, $"{Name} has {XP} XP. This means they are now level {Level}. Their health is {CurrentHealth}. Attack power {AttackPower}");
    }

    public void SetAvatar(Sprite avatar)
    {
        Avatar = avatar;
    }
}

