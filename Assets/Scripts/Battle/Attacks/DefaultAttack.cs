using UnityEngine;
using UnityEngine.AddressableAssets;

public class DefaultAttack : IAttack
{
    public string Name { get; } = "Default Attack";
    public IActor Attacker { get; private set; }
    public IActor Target { get; private set; }

    public IAttack WithAttacker(IActor attacker)
    {
        Attacker = attacker;
        return this;
    }

    public IAttack WithTarget(IActor target)
    {
        Target = target;
        return this;
    }
}
