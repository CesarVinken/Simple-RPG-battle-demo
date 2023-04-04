using UnityEngine;

public class Hero : IHero
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public float Health { get; private set; }
    public float MaxHealth { get; private set; }
    public float AttackPower { get; private set; }
    public int XP { get; private set; }
    public int Level { get; private set; }
    public Sprite Avatar { get; private set; }

    public Hero(HeroBlueprint heroBlueprint)
    {
        Id = heroBlueprint.Id;
        Name = heroBlueprint.Name;

        XP = 0;
        Level = 1;
        MaxHealth = 100;
        Health = MaxHealth;
        AttackPower = 10;
    }

    public Hero WithXP(int xp)
    {
        // calculate level, xp, attack
        return this;
    }

    public void SetAvatar(Sprite avatar)
    {
        Avatar = avatar;
    }
}
