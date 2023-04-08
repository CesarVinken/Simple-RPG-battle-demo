using UnityEngine;

public class Enemy : IEnemy
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }
    public float AttackPower { get; private set; }
    public int Level { get; private set; }

    public Sprite Avatar { get; private set; }

    public Enemy(EnemyBlueprint enemyBlueprint)
    {
        Id = enemyBlueprint.Id;
        Name = enemyBlueprint.Name;

        Level = 1;
        MaxHealth = 100;
        CurrentHealth = MaxHealth;
        AttackPower = 10;
    }

    public void SetAvatar(Sprite avatar)
    {
        Avatar = avatar;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            ConsoleLog.Log(LogCategory.General, $"The enemy {Name} was defeated");

            GameEventHandler.GetInstance().ExecuteHasTakenDamageEvent(this);
            GameEventHandler.GetInstance().ExecuteEnemyDefeatedEvent();
            return;
        }

        GameEventHandler.GetInstance().ExecuteHasTakenDamageEvent(this);
    }
}
