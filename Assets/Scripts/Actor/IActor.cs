using System;
using UnityEngine;

public interface IActor
{
    int Id { get; }
    string Name { get; }
    float MaxHealth { get; }
    float CurrentHealth { get; }
    float AttackPower { get; }
    int Level { get; }
    Sprite Avatar { get; }

    void SetAvatar(Sprite avatar);
    void TakeDamage(float damage);
}
