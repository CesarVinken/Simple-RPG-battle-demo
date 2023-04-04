using UnityEngine;

public interface IActor
{
    int Id { get; }
    string Name { get; }
    float Health { get; }
    float AttackPower { get; }
    int Level { get; }
    Sprite Avatar { get; }

    void SetAvatar(Sprite avatar);
}
